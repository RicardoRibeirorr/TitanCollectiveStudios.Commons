using UnityEngine;


namespace TitanCollectiveStudios.Commons.Managers
{
    public class ConfigManager : GameSystem<ConfigManager>
    {

        /***************************************************
         *                  LAYER MASK
         **************************************************/
        [Header("Masks (Interactions, Raycasts, etc")]
        [SerializeField] LayerMask _entityMask;
        [SerializeField] LayerMask _vehicleMask;
        [SerializeField] LayerMask _worldInteractableMask;
        [SerializeField] LayerMask _ignoreMask;
        LayerMask _defaultInteractableMask;
        public LayerMask EntityMask => _entityMask;
        public LayerMask VehicleMask => _vehicleMask;
        public LayerMask WorldInteractableMask => _worldInteractableMask;
        public LayerMask DefaultInteractableMask => _entityMask | _vehicleMask | _worldInteractableMask;
        public LayerMask DefaultObstacleMask { get; private set; }
        public LayerMask GetMasksNotIn(LayerMask all, LayerMask exclude) => all & ~exclude;
        private void HandleMaskSetup()
        {
            LayerMask allChecked = ~0;
            DefaultObstacleMask = GetMasksNotIn(allChecked, _ignoreMask);
        }

        protected void Start()
        {
            HandleMaskSetup();
        }
    }
}
