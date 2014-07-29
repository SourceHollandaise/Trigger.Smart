using System.Collections.Generic;
using System;

namespace Trigger.XForms
{
    public abstract class ViewDescriptor
    {
        public const string EmptySpaceFieldName = "EmptySpace";

        public IList<GroupItemDescription> GroupItemDescriptions { get; set; }

        public IList<TabItemDescription> TabItemDescriptions { get; set; }
    }

    public static class ViewDescriptorDeclarator
    {
        static readonly Dictionary<Type, Type> descriptors = new Dictionary<Type, Type>();

        public static void Declare<T,U>()
        {
            if (!descriptors.ContainsKey(typeof(T)))
                descriptors.Add(typeof(T), typeof(U));
        }

        public static Type GetDescriptor(Type type)
        {
            if (descriptors.ContainsKey(type))
                return descriptors[type];
            return null;
        }
    }
}

