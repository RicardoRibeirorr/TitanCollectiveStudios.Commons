using System;

namespace TitanCollectiveStudios.Commons.Conditions
{
    public interface iCondition
    {
        public bool Evaluate();
        public void StartListening(Action onStateChanged);
        public void StopListening();
    }
}
