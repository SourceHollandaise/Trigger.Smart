using System;

namespace Trigger.XStorable.Dependency
{
	/// <summary>
	/// Default dependency attribute. Marks a class as default class for injection. If attribute is set on class,the class must not registered via map.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, Inherited = true)]
	public sealed class DefaultDependencyAttribute : Attribute
	{
		/// <summary>
		/// Gets the type of the interface.
		/// </summary>
		/// <value>The type of the interface.</value>
		public Type InterfaceType { get; private set; }

		public DefaultDependencyAttribute(Type interfaceType)
		{
			InterfaceType = interfaceType;
		}
	}
}
