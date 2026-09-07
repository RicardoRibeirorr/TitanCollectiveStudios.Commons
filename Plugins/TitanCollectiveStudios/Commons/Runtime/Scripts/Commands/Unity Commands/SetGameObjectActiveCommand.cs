using MackySoft.SerializeReferenceExtensions;
using TitanCollectiveStudios.Commons.Commands;
using UnityEngine;

namespace TitanCollectiveStudios.QuestSystem.Commands
{
    /// <summary>
    /// Command to activate or deactivate a GameObject
    /// </summary>
    [AddTypeMenu("Unity/Gameobject Enable_Disable")]
    [System.Serializable]
    public class SetGameObjectActiveCommand : UnityCommand
    {
        [SerializeField] private GameObject targetGameObject;
        [SerializeField] private bool setActive = true;
        private bool previousState;

        public GameObject TargetGameObject
        {
            get => targetGameObject;
            set => targetGameObject = value;
        }

        public bool SetActive
        {
            get => setActive;
            set => setActive = value;
        }

        public override void Execute()
        {
            if (targetGameObject == null)
            {
                Debug.LogWarning("SetGameObjectActiveCommand: Target GameObject is null!");
                return;
            }

            previousState = targetGameObject.activeSelf;
            targetGameObject.SetActive(setActive);
            Debug.Log($"GameObject '{targetGameObject.name}' set to {setActive}");
        }

        public override void Undo()
        {
            if (targetGameObject != null)
                targetGameObject.SetActive(previousState);
        }
    }
}
