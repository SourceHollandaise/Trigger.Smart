using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Trigger.Dependency
{
    public sealed class DependencyMap : IDependencyMap, IDisposable
    {
        Dictionary<Type, object> registeredInstances = new Dictionary<Type, object>();
        Dictionary<Type, Type> registeredTypes = new Dictionary<Type, Type>();

        public void RegisterDefaultDependencies(IEnumerable<Assembly> assemblies)
        {
            if (assemblies == null)
                throw new NullReferenceException("At least one assembly must be set!");

            foreach (var assembly in assemblies)
            {
                var types = assembly.GetTypes().Where(p => p.IsClass && p.GetCustomAttributes(typeof(DefaultDependencyAttribute), true).FirstOrDefault() != null);

                foreach (var type in types)
                {
                    var attribute = (DefaultDependencyAttribute)type.GetCustomAttributes(typeof(DefaultDependencyAttribute), true).FirstOrDefault();

                    RegisterType(attribute.InterfaceType, type);
                }
            }
        }

        public void RegisterInstance<T>(object instance)
        {
            if (instance == null)
                throw new NullReferenceException("Instance to register is null!");
                
            if (!registeredInstances.ContainsKey(typeof(T)))
                registeredInstances.Add(typeof(T), instance);
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
            if (registeredInstances.ContainsKey(typeof(T)))
                return (T)registeredInstances[typeof(T)];

            throw new ArgumentException(string.Format("Instance of type '{0}' is not registered!", typeof(T)));
        }

        public object ResolveInstance(Type type)
        {
            if (registeredInstances.ContainsKey(type))
                return registeredInstances[type];

            throw new ArgumentException(string.Format("Instance of type '{0}' is not registered!", type));
        }

        public void UnregisterInstance<T>()
        {
            if (registeredInstances.ContainsKey(typeof(T)))
                registeredInstances.Remove(typeof(T));
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
            if (!typeof(T).IsInterface)
                throw new ArgumentException(string.Format("'{0}' is not an interface!", typeof(T)));

            if (registeredTypes.ContainsKey(typeof(T)))
                registeredTypes.Remove(typeof(T));

            registeredTypes.Add(typeof(T), typeof(U));
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
            if (registeredTypes.ContainsKey(typeof(T)))
            {
                var instance = registeredTypes[typeof(T)];

                if (instance.GetInterface(typeof(T).Name, false) == typeof(T))
                    return (T)Activator.CreateInstance(instance);

                throw new ArgumentException(string.Format("'{0}' is not type of '{1}'", instance, typeof(T)));
            }

            throw new ArgumentException(string.Format("Cannot create an instance of interface '{0}'", typeof(T)));
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
            if (registeredTypes.ContainsKey(typeof(T)))
                registeredTypes.Remove(typeof(T));
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
