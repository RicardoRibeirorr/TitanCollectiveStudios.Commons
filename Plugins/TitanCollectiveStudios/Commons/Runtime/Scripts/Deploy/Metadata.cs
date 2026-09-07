namespace TitanCollectiveStudios.Commons.Deploys
{

    /// <summary>
    /// Metadata contains non-gameplay information about a save file and the application state.
    /// 
    /// It is used to describe and identify a save (e.g. version, timestamp, display info)
    /// without storing any runtime gameplay or world simulation data.
    /// 
    /// This keeps save data cleanly separated between "what the player has done"
    /// and "how the save is identified and presented to the user".
    /// </summary>
    [System.Serializable]
    public class Metadata
    {
        public string version = "1.0.0";
    }
}
