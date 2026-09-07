using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace TitanCollectiveStudios.Commons.Inspectors.Editor
{

    static class BindMethods
    {
        public static void Bind(MonoBehaviour behaviour)
        {
            var type = behaviour.GetType();

            // Fields
            foreach (var field in type.GetFields(
                BindingFlags.Instance |
                BindingFlags.Public |
                BindingFlags.NonPublic))
            {
                var attribute = field.GetCustomAttribute<BindAttribute>();

                if (attribute == null)
                    continue;

                if (field.GetValue(behaviour) != null)
                    continue;

                var component = FindComponent(
                    behaviour,
                    field.FieldType,
                    attribute);

                if (component != null)
                    field.SetValue(behaviour, component);
            }


            // Properties
            foreach (var property in type.GetProperties(
                BindingFlags.Instance |
                BindingFlags.Public |
                BindingFlags.NonPublic))
            {
                var attribute = property.GetCustomAttribute<BindAttribute>();

                if (attribute == null)
                    continue;

                if (!property.CanWrite)
                    continue;

                if (property.GetValue(behaviour) != null)
                    continue;

                var component = FindComponent(
                    behaviour,
                    property.PropertyType,
                    attribute);

                if (component != null)
                    property.SetValue(behaviour, component);
            }
        }

        internal static Component FindComponent(
           MonoBehaviour behaviour,
           System.Type type,
           BindAttribute attribute)
        {
            switch (attribute.Mode)
            {
                case BindMode.Self:
                    return behaviour.GetComponent(type);

                case BindMode.Parent:
                    return behaviour.GetComponentInParent(type, attribute.IncludeInactive);

                case BindMode.Children:
                    return behaviour.GetComponentInChildren(type, attribute.IncludeInactive);

                case BindMode.Anywhere:

                    var self = behaviour.GetComponent(type);

                    if (self != null)
                        return self;

                    var child = behaviour.GetComponentInChildren(type, attribute.IncludeInactive);

                    if (child != null)
                        return child;

                    return behaviour.GetComponentInParent(type, attribute.IncludeInactive);
            }

            return null;
        }


        internal static class BindCache
        {
            private static readonly Dictionary<Type, FieldInfo[]> cache = new();

            public static FieldInfo[] GetFields(Type type)
            {
                Type originalType = type;

                if (cache.TryGetValue(originalType, out var fields))
                    return fields;

                List<FieldInfo> result = new();

                while (type != null && type != typeof(object))
                {
                    result.AddRange(type.GetFields(
                        BindingFlags.Instance |
                        BindingFlags.Public |
                        BindingFlags.NonPublic |
                        BindingFlags.DeclaredOnly));

                    type = type.BaseType;
                }

                fields = result.ToArray();

                cache[originalType] = fields;

                return fields;
            }
        }
    }
}