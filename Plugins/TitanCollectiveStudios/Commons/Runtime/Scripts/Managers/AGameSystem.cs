using TitanCollectiveStudios.Commons.Core;

namespace TitanCollectiveStudios.Commons.Managers
{
    public interface iGameSystem { }
    public abstract class AGameSystem<T> : Standalone<T>, iGameSystem where T : AGameSystem<T>
    {
        //public virtual void OnSystemInit() { }

        //protected override void Awake()
        //{
        //    base.Awake();
        //    AGameManager.i.RegisterSystem(this);
        //}
    }
}
