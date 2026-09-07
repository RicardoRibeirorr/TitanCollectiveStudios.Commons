using MackySoft.SerializeReferenceExtensions;
using TitanCollectiveStudios.Commons.Commands;
using UnityEngine;

namespace TitanCollectiveStudios.QuestSystem.Commands
{
    /// <summary>
    /// Command to trigger multiple colliders/triggers
    /// </summary>
    [AddTypeMenu("Unity/Enable_Disable Collider")]
    [System.Serializable]
    public class DisableColliderCommand : UnityCommand
    {
        [SerializeField] private GameObject targetGameObject;
        [SerializeField] private bool disableAllColliders = true;
        private bool[] previousStates;

        public GameObject TargetGameObject
        {
            get => targetGameObject;
            set => targetGameObject = value;
        }

        public bool DisableAllColliders
        {
            get => disableAllColliders;
            set => disableAllColliders = value;
        }

        public override void Execute()
        {
            if (targetGameObject == null)
            {
                Debug.LogWarning("DisableColliderCommand: Target GameObject is null!");
                return;
            }

            var colliders = targetGameObject.GetComponents<Collider>();
            previousStates = new bool[colliders.Length];

            for (int i = 0; i < colliders.Length; i++)
            {
                previousStates[i] = colliders[i].enabled;
                colliders[i].enabled = false;
            }

            Debug.Log($"Disabled {colliders.Length} colliders on '{targetGameObject.name}'");
        }

        public override void Undo()
        {
            if (targetGameObject == null) return;

            var colliders = targetGameObject.GetComponents<Collider>();
            if (previousStates != null && previousStates.Length == colliders.Length)
            {
                for (int i = 0; i < colliders.Length; i++)
                    colliders[i].enabled = previousStates[i];
            }
        }
    }
}
