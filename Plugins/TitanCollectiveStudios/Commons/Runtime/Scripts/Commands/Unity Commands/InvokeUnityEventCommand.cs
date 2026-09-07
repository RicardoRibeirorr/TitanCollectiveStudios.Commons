using MackySoft.SerializeReferenceExtensions;
using TitanCollectiveStudios.Commons.Commands;
using UnityEngine;
using UnityEngine.Events;

namespace TitanCollectiveStudios.QuestSystem.Commands
{
    /// <summary>
    /// Command to invoke a UnityEvent
    /// </summary>
    [AddTypeMenu("Unity/Invoke Unity Event")]
    [System.Serializable]
    public class InvokeUnityEventCommand : UnityCommand
    {
        [SerializeField] private UnityEvent unityEvent = new UnityEvent();

        public UnityEvent UnityEvent
        {
            get => unityEvent;
            set => unityEvent = value;
        }
        public override void Execute()
        {
            unityEvent?.Invoke();
            Debug.Log("UnityEvent invoked");
        }
    }
}
