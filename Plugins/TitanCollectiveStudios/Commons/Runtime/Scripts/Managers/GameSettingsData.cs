using System;
using System.Collections.Generic;
using System.Text;
using TitanCollectiveStudios.Commons.Datas;
using UnityEngine;

namespace Assets.Plugins.TitanCollectiveStudios.Commons.Runtime.Scripts.Managers
{
    [CreateAssetMenu(fileName = "New Game Settings", menuName = "TitanCollectiveStudios/Game Settings")]
    public class GameSettingsData : DataAsset
    {
        [Header("Graphics Settings")]
        public float viewDistance = 400;
        public float render3DDistance = 200;
        public float renderLightDistance = 150;
    }

}
