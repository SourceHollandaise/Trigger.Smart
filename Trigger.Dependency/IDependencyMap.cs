using System;
using System.Reflection;
using System.Collections.Generic;

namespace Trigger.XStorable.Dependency
{
	/// <summary>
	/// Base Interface fpr DependencyMap
	/// </summary>
	public interface IDependencyMap : IDisposable
	{
		/// <summary>
		/// Registers the default dependencies for target assemblies.
		/// </summary>
		/// <param name="assemblies">Assemblies.</param>
		void RegisterDefaultDependencies(IEnumerable<Assembly> assemblies);

		/// <summary>
		/// Register the instance.
		/// </summary>
		/// <param name="instance">Instance.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		void RegisterInstance<T>(object instance);

		/// <summary>
		/// Registers the instance.
		/// </summary>
		/// <param name="type">Type.</param>
		/// <param name="instance">Instance.</param>
		void RegisterInstance(Type type, object instance);

		/// <summary>
		/// Resolve the instance.
		/// </summary>
		/// <returns>The instance.</returns>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		T ResolveInstance<T>();

		/// <summary>
		/// Resolves the instance.
		/// </summary>
		/// <returns>The instance.</returns>
		/// <param name="type">Type.</param>
		object ResolveInstance(Type type);

		/// <summary>
		/// Registers the type.
		/// </summary>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		/// <typeparam name="U">The 2nd type parameter.</typeparam>
		void RegisterType<T, U>();

		/// <summary>
		/// Registers the type.
		/// </summary>
		/// <param name="interfaceType">Interface type.</param>
		/// <param name="classType">Class type.</param>
		void RegisterType(Type interfaceType, Type classType);

		/// <summary>
		/// Resolve the type.
		/// </summary>
		/// <returns>The type.</returns>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		T ResolveType<T>();

		/// <summary>
		/// Resolves the type.
		/// </summary>
		/// <returns>The type.</returns>
		/// <param name="type">Type.</param>
		object ResolveType(Type type);

		/// <summary>
		/// Clear the registered instances collection.
		/// </summary>
		void ClearRegisteredInstancesCollection();

		/// <summary>
		/// Unregister the instance.
		/// </summary>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		void UnregisterInstance<T>();

		/// <summary>
		/// Unregisters the instance.
		/// </summary>
		/// <param name="type">Type.</param>
		void UnregisterInstance(Type type);

		/// <summary>
		/// Clears the registered types collection.
		/// </summary>
		void ClearRegisteredTypesCollection();

		/// <summary>
		/// Unregisters the type.
		/// </summary>
		/// <param name="type">Type.</param>
		void UnregisterType(Type type);

		/// <summary>
		/// Unregister the type.
		/// </summary>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		void UnregisterType<T>();
	}
}
