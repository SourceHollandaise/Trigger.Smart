using System;
using System.Collections.Generic;
using XForms.Store;

namespace XForms.Design
{
    public static class DetailViewDescriptorProvider
    {
        static readonly Dictionary<Type, Type> descriptors = new Dictionary<Type, Type>();

        public static void Declare<TStorable,TDescriptor>() where TStorable: IStorable where TDescriptor: IDetailViewDescriptor
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
