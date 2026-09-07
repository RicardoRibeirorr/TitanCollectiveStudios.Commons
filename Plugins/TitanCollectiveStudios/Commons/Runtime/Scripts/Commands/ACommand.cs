using System;
using System.Collections;
using UnityEngine;


namespace TitanCollectiveStudios.Commons.Commands
{
    [Serializable]
    public abstract class ACommand : iCommand
    {
        public abstract void Execute();
        public virtual void Undo() { }

        public IEnumerator WaitExecute()
        {
            Execute();
            yield return new WaitForEndOfFrame();
        }
    }
}
