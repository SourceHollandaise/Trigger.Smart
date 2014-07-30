using System.Collections.Generic;
using System;
using Trigger.XStorable.DataStore;

namespace Trigger.XForms
{
    public static class ViewDescriptorProvider
    {
        static readonly Dictionary<Type, Type> descriptors = new Dictionary<Type, Type>();

        public static void Declare<TStorable,TDescriptor>() where TStorable: IStorable where TDescriptor: IViewDescriptor
        {
            if (!descriptors.ContainsKey(typeof(TStorable)))
                descriptors.Add(typeof(TStorable), typeof(TDescriptor));
        }

        public static Type GetDescriptor(Type type)
        {
            return descriptors.ContainsKey(type) ? descriptors[type] : null;
        }
    }

    public static class ListDescriptorProvider
    {
        static readonly Dictionary<Type, Type> descriptors = new Dictionary<Type, Type>();

        public static void Declare<TStorable,TDescriptor>() where TStorable: IStorable where TDescriptor: IListDescriptor
        {
            if (!descriptors.ContainsKey(typeof(TStorable)))
                descriptors.Add(typeof(TStorable), typeof(TDescriptor));
        }

        public static Type GetDescriptor(Type type)
        {
            return descriptors.ContainsKey(type) ? descriptors[type] : null;
        }
    }
}
