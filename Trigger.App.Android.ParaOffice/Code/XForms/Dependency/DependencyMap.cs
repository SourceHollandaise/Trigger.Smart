using System;
using System.Collections.Generic;

namespace XForms.Dependency
{
    public sealed class DependencyMap : IDependencyMap, IDisposable
    {
        Dictionary<Type, object> registeredInstances = new Dictionary<Type, object>();
        Dictionary<Type, Type> registeredTypes = new Dictionary<Type, Type>();

        public void RegisterInstance<T>(object instance)
        {
            RegisterInstance(typeof(T), instance);
        }

        public void RegisterInstance(Type type, object instance)
        {
            if (instance == null)
                throw new NullReferenceException("Instance to register is null!");

            if (!registeredInstances.ContainsKey(type))
                registeredInstances.Add(type, instance);
        }

        public T ResolveInstance<T>()
        {
            return (T)ResolveInstance(typeof(T));
        }

        public object ResolveInstance(Type type)
        {
            if (registeredInstances.ContainsKey(type))
                return registeredInstances[type];

            throw new ArgumentException(string.Format("Instance of type '{0}' is not registered!", type));
        }

        public void UnregisterInstance<T>()
        {
            UnregisterInstance(typeof(T));
        }

        public void UnregisterInstance(Type type)
        {
            if (registeredInstances.ContainsKey(type))
                registeredInstances.Remove(type);
        }

        public void ClearRegisteredInstancesCollection()
        {
            registeredInstances.Clear();
        }

        public void RegisterType<T, U>()
        {
            RegisterType(typeof(T), typeof(U));
        }

        public void RegisterType(Type interfaceType, Type classType)
        {
            if (!interfaceType.IsInterface)
                throw new ArgumentException(string.Format("'{0}' is not an interface!", interfaceType));

            if (registeredTypes.ContainsKey(interfaceType))
                registeredTypes.Remove(interfaceType);

            registeredTypes.Add(interfaceType, classType);
        }

        public T ResolveType<T>()
        {
            return (T)ResolveType(typeof(T));
        }

        public object ResolveType(Type type)
        {
            if (registeredTypes.ContainsKey(type))
            {
                var instance = registeredTypes[type];

                if (instance.GetInterface(type.Name, false) == type)
                    return Activator.CreateInstance(instance);

                throw new ArgumentException(string.Format("'{0}' is not type of '{1}'", instance, type));
            }

            throw new ArgumentException(string.Format("Cannot create an instance of interface '{0}'", type));
        }

        public void UnregisterType<T>()
        {
            UnregisterType(typeof(T));
        }

        public void UnregisterType(Type type)
        {
            if (registeredTypes.ContainsKey(type))
                registeredTypes.Remove(type);
        }

        public void ClearRegisteredTypesCollection()
        {
            registeredTypes.Clear();
        }

        public void Dispose()
        {
            registeredTypes = null;
            registeredInstances = null;
        }
    }
}
