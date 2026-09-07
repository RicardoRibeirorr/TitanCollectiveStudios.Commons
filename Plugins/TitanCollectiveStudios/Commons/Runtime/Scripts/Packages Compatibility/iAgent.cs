using Assets.Plugins.TitanCollectiveStudios.Commons.Runtime.Scripts.Managers;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;


namespace TitanCollectiveStudios.Commons.Packages
{
    public interface iAgent
    {
        bool IsPlayer { get; }
        List<string> Flags{ get; }
        iAgentBlackbox Blackbox { get; }
     }
}

