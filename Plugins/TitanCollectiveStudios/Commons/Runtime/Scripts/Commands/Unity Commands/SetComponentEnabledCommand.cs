using MackySoft.SerializeReferenceExtensions;
using TitanCollectiveStudios.Commons.Commands;
using UnityEngine;

namespace TitanCollectiveStudios.QuestSystem.Commands
{
    /// <summary>
    /// Command to enable or disable a component
    /// </summary>
    [AddTypeMenu("Unity/Component Enable_Disable")]
    [System.Serializable]
    public class SetComponentEnabledCommand : UnityCommand
    {
        [SerializeField] private GameObject targetGameObject;
        [SerializeField] private string componentTypeName = "Collider";
        [SerializeField] private bool setEnabled = true;
        private bool previousState;

        public GameObject TargetGameObject
        {
            get => targetGameObject;
            set => targetGameObject = value;
        }

        public string ComponentTypeName
        {
            get => componentTypeName;
            set => componentTypeName = value;
        }

        public bool SetEnabled
        {
            get => setEnabled;
            set => setEnabled = value;
        }

        public override void Execute()
        {
            if (targetGameObject == null)
            {
                Debug.LogWarning("SetComponentEnabledCommand: Target GameObject is null!");
                return;
            }

            var component = targetGameObject.GetComponent(componentTypeName) as Behaviour;
            if (component == null)
            {
                Debug.LogWarning($"SetComponentEnabledCommand: Component '{componentTypeName}' not found on '{targetGameObject.name}'!");
                return;
            }

            previousState = component.enabled;
            component.enabled = setEnabled;
            Debug.Log($"Component '{componentTypeName}' on '{targetGameObject.name}' set to {setEnabled}");
        }

        public override void Undo()
        {
            if (targetGameObject != null)
            {
                var component = targetGameObject.GetComponent(componentTypeName) as Behaviour;
                if (component != null)
                    component.enabled = previousState;
            }
        }
    }
}
