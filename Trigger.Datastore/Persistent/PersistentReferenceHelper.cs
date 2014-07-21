using System;
using System.Linq;
using Trigger.Dependency;

namespace Trigger.Datastore.Persistent
{

	public static class PersistentReferenceHelper
	{
		static IStore Store
		{
			get
			{
				return DependencyMapProvider.Instance.ResolveType<IStore>();
			}
		}

		public static void UpdatePersistentReferences(IPersistent persistent)
		{
			var properties = persistent.GetType().GetProperties().AsEnumerable()
                .Where(p => p.GetCustomAttributes(typeof(PersistentReferenceAttribute), true).Length > 0).ToList();

			foreach (var property in properties)
			{
				var propValue = property.GetValue(persistent, null);

				if (propValue != null)
				{
					var persistentRef = propValue as IPersistent;
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
