using System;

namespace TitanCollectiveStudios.Commons.Datas
{
    [Serializable]
    public abstract class Definition<TData>
        where TData : DataAsset
    {
        protected TData data;

        public TData Data => data;

        protected Definition(TData data)
        {
            this.data = data;
        }

        public string Id => data.Id;
    }
}