using MackySoft.SerializeReferenceExtensions;
using TitanCollectiveStudios.Commons.Commands;
using UnityEngine;

namespace TitanCollectiveStudios.QuestSystem.Commands
{
    /// <summary>
    /// Command to move a GameObject to a specific position
    /// </summary>
    [AddTypeMenu("Unity/Move GameObject")]
    [System.Serializable]
    public class MoveGameObjectCommand : UnityCommand
    {
        [SerializeField] private GameObject targetGameObject;
        [SerializeField] private Vector3 targetPosition;
        [SerializeField] private bool useWorldSpace = true;
        private Vector3 previousPosition;

        public GameObject TargetGameObject
        {
            get => targetGameObject;
            set => targetGameObject = value;
        }

        public Vector3 TargetPosition
        {
            get => targetPosition;
            set => targetPosition = value;
        }

        public bool UseWorldSpace
        {
            get => useWorldSpace;
            set => useWorldSpace = value;
        }

        public override void Execute()
        {
            if (targetGameObject == null)
            {
                Debug.LogWarning("MoveGameObjectCommand: Target GameObject is null!");
                return;
            }

            var transform = targetGameObject.transform;
            previousPosition = useWorldSpace ? transform.position : transform.localPosition;

            if (useWorldSpace)
                transform.position = targetPosition;
            else
                transform.localPosition = targetPosition;

            Debug.Log($"Moved '{targetGameObject.name}' to {targetPosition}");
        }

        public override void Undo()
        {
            if (targetGameObject == null) return;

            var transform = targetGameObject.transform;
            if (useWorldSpace)
                transform.position = previousPosition;
            else
                transform.localPosition = previousPosition;
        }
    }
}
