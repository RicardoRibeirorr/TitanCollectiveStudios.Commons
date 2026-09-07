namespace TitanCollectiveStudios.Commons.Managers
{

    //All saveable systems need to implement this
    public interface ISaveableSystem<T> : ISaveableSystemMarker
    {
        string Id => typeof(T).FullName!;

        T OnSave();
        void OnLoad(T state);
    }

    //This is just a marker, it helps 
    public interface ISaveableSystemMarker { }
}