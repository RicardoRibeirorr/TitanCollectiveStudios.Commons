using System;
using UnityEngine;


namespace TitanCollectiveStudios.Commons.Commands
{
    public interface iBaseCommand : iCommand { }

    [Serializable]
    public abstract class BaseCommand : ACommand, iBaseCommand
    {
    }
}
