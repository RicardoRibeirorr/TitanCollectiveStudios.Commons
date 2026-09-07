using UnityEngine;


namespace TitanCollectiveStudios.Commons.Utils
{
    public class Vector2Util : MonoBehaviour
    {
        /// <summary>
        /// Converts analog/free input into cardinal directions.
        /// Used for example for 2d movement (tiled VS free)
        /// 
        /// Example:
        /// (0.7,0.2) -> (1,0)
        /// (0.2,0.8) -> (0,1)
        /// </summary>
        public static Vector2 SnapDirection(Vector2 input)
        {
            if (input == Vector2.zero)
                return Vector2.zero;

            if (Mathf.Abs(input.x) > Mathf.Abs(input.y))
                return new Vector2(Mathf.Sign(input.x), 0);

            return new Vector2(0, Mathf.Sign(input.y));
        }
    }
}
