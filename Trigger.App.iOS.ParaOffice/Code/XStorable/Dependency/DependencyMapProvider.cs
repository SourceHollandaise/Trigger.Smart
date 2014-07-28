

namespace Trigger.XStorable.Dependency
{
	/// <summary>
	/// DependencyMapProvider.
	/// </summary>
	public static class DependencyMapProvider
	{
		readonly static object _lock = new object();
		
		static IDependencyMap map;

		/// <summary>
		/// Gets the instance.
		/// </summary>
		/// <value>The instance.</value>
		public static IDependencyMap Instance
		{
			get
			{
				lock (_lock)
				{
					if (map == null)
						map = new DependencyMap();
					return map;
				}
			}
		}

		/// <summary>
		/// Destroy this instance.
		/// </summary>
		public static void Destroy()
		{
			map = null;
		}
	}
}

