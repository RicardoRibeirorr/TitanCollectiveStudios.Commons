using UnityEngine;


namespace TitanCollectiveStudios.Commons.Managers
{


    public class MinimapSystem : GameSystem<MinimapSystem>
    {
        [SerializeField] private Camera _minimapCamera;

        public Camera MinimapCamera => _minimapCamera;
    }
}
