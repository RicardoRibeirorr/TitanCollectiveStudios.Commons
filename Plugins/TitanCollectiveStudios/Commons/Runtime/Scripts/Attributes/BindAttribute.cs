using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;


namespace TitanCollectiveStudios.Commons.Inspectors
{
    /// <summary>
    /// Simillar to <b>this.getComponent(X)</b> but handled in editor, allows non serializable properties,
    /// is no code, faster, more performance, and allow debug without Play mode.
    /// </summary>
    /// <remarks>
    /// Allows privates non serializables. Open debug mode to verify it.
    /// </remarks>
    /// <example>
    /// <code>
    /// 

    public enum BindMode
    {
        Self,
        Parent,
        Children,
        Anywhere
    }

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, Inherited = true)]
    public class BindAttribute : PropertyAttribute
    {
        public BindMode Mode { get; }

        public bool Optional { get; set; }

        public bool IncludeInactive { get; set; }

        public BindAttribute(BindMode mode = BindMode.Self)
        {
            Mode = mode;
        }
    }
}