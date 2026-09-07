using System;
using UnityEngine;




namespace TitanCollectiveStudios.Commons.Commands
{
    public interface iUnityCommand : iCommand { }
    [Serializable]
    public abstract class UnityCommand : ACommand, iUnityCommand
    {
    }
}