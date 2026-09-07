using TitanCollectiveStudios.GameManagers;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;


namespace TitanCollectiveStudios.Commons.Managers
{
    public class SaveSystem : GameSystem<SaveSystem>
    {
        private const string FILE_NAME = "savegame.json";

        private System.Collections.Generic.List<object> _systems = new();

        protected void Start()
        {
            RegisterSystems();
        }


        #region Registration

        private void RegisterSystems()
        {
            if (_systems.Count() <= 0)
            {
                _systems = GameManager.i._systems
                    .Where(x => x is ISaveableSystemMarker)
                    .Cast<object>()
                    .ToList();
            }
        }

        #endregion

        #region SAVE

        public void SaveGame()
        {
            GameSaveData save = new GameSaveData();


            foreach (var system in _systems)
            {
                SaveSystemObject(save, system);
            }

            string json = JsonUtility.ToJson(save, true);
            string path = GetPath();
            File.WriteAllText(path, json);

            Debug.Log("[SaveSystem] Saved at path: " + path);
        }

        private void SaveSystemObject(GameSaveData save, object system)
        {
            Type type = system.GetType();

            foreach (var iface in type.GetInterfaces())
            {
                if (!iface.IsGenericType) continue;
                if (iface.GetGenericTypeDefinition() != typeof(ISaveableSystem<>)) continue;

                MethodInfo onSave = iface.GetMethod("OnSave");
                object data = onSave.Invoke(system, null);

                string jsonData = JsonUtility.ToJson(data);

                save.systems.Add(new SaveEntryData
                {
                    systemId = type.FullName,
                    jsonData = jsonData
                });

                return;
            }
        }

        #endregion

        #region LOAD

        public void LoadGame()
        {
            string path = GetPath();

            if (!File.Exists(path))
            {
                throw new Exception("No save found");
            }

            string json = File.ReadAllText(path);
            GameSaveData save = JsonUtility.FromJson<GameSaveData>(json);

            foreach (var entry in save.systems)
            {
                LoadSystemObject(entry);
            }

            Debug.Log("[SaveSystem] Loaded");
        }

        private void LoadSystemObject(SaveEntryData entry)
        {
            foreach (var system in _systems)
            {
                Type type = system.GetType();

                if (type.FullName != entry.systemId)
                    continue;

                foreach (var iface in type.GetInterfaces())
                {
                    if (!iface.IsGenericType) continue;
                    if (iface.GetGenericTypeDefinition() != typeof(ISaveableSystem<>)) continue;

                    Type saveType = iface.GetGenericArguments()[0];

                    object data = JsonUtility.FromJson(entry.jsonData, saveType);

                    MethodInfo onLoad = iface.GetMethod("OnLoad");
                    onLoad.Invoke(system, new[] { data });

                    return;
                }
            }
        }

        #endregion

        #region PATH

        private string GetPath()
        {
            return Path.Combine(Application.persistentDataPath, FILE_NAME);
        }

        #endregion
    }
}