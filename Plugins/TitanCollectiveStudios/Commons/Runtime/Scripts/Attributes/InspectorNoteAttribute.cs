using UnityEngine;

namespace TitanCollectiveStudios.Commons.Inspectors
{
    public class InspectorNoteAttribute : PropertyAttribute
    {
        public readonly string Message;

        public InspectorNoteAttribute(string message)
        {
            Message = message;
        }
    }
}