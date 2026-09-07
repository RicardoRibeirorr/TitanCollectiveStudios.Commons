namespace TitanCollectiveStudios
{
    public interface ISaveable<T>
    {
        T OnSave();
        void OnLoad(T state);
    }
}
