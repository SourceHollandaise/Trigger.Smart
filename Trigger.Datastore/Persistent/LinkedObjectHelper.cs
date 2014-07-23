using System;
using System.Linq;
using Trigger.Dependency;

namespace Trigger.Datastore.Persistent
{

	public static class LinkedObjectHelper
	{
		static IStore Store
		{
			get
			{
				return DependencyMapProvider.Instance.ResolveType<IStore>();
			}
		}

		public static void UpdatePersistentReferences(IStorable persistent)
		{
			var properties = persistent.GetType().GetProperties().AsEnumerable()
                .Where(p => p.GetCustomAttributes(typeof(LinkedObjectAttribute), true).Length > 0).ToList();

			foreach (var property in properties)
			{
				var propValue = property.GetValue(persistent, null);

				if (propValue != null)
				{
					var persistentRef = propValue as IStorable;
					if (persistentRef != null)
					{
						var persistentFromStore = Store.Load(persistentRef.GetType(), persistentRef.MappingId);

						if (persistentFromStore == null)
						{
							property.SetValue(persistent, null, null);
						}
					}
				}
			}
		}
	}
}
