using System;
using System.Collections.Generic;
using TitanCollectiveStudios.Commons.Deploys;


namespace TitanCollectiveStudios.Commons.Managers
{
    [Serializable]
    public class GameSaveData : Metadata
    {
        public List<SaveEntryData> systems = new();
    }

    [Serializable]
    public class SaveEntryData
    {
        public string systemId;
        public string jsonData;
    }
}