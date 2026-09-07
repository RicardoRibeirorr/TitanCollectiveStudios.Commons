using TitanCollectiveStudios.Commons.Packages;
using UnityEngine;

namespace TitanCollectiveStudios.Modules
{
    public interface iBaseMovementBlackboard : iAgentBlackbox
    {
        ///// <summary> Input for movement was pressed (ex: WASD on pc) </summary>
        public Vector2 MoveInput { get; set; }
        ///// <summary> Input for sprinting was pressed </summary>
        public bool SprintInput { get; set; }



        ///// <summary> Indicates the agent is moving </summary>
        public bool IsMoving { get; set; }
        ///// <summary> Indicates the agent is moving </summary>
        public Vector2 MovementDirection { get; set; }
        /// <summary> Speed of the character </summary>
        public float Speed { get; set; }
        /// <summary> Acceleation from and to idle to movement etc </summary>
        public float Acceleration { get; set; }
        /// <summary> Sprint Speed of the character. Multiply speed by SprintSpeedMultiplier </summary>
        public float SprintSpeedMultiplier { get; set; }
        /// <summary> Speed of the character turning </summary>
        public float TurnSpeed { get; set; }
    }
}