using System;
using Trigger.Dependency;
using Trigger.Datastore.Security;
using System.Linq;
using System.Collections.Generic;

namespace Trigger.Datastore.Persistent
{
	public class LinkedObjectsProvider
	{
		protected IPersistent CurrentObject
		{
			get;
			set;
		}

		public LinkedObjectsProvider(IPersistent currentObject)
		{
			this.CurrentObject = currentObject;
		}

		public IEnumerable<IEnumerable<IPersistent>> LinkedObjectLists()
		{
			var store = DependencyMapProvider.Instance.ResolveType<IStore>();

			var properties = CurrentObject.GetType().GetProperties().Where(p => typeof(IPersistent).IsAssignableFrom(p.PropertyType));

			foreach (var property in properties)
			{
				IPersistent value = property.GetValue(CurrentObject, null) as IPersistent;
				if (value != null)
				{
					var List = store.LoadAll(property.GetType()).Where(p => p.MappingId != null && p.MappingId == value.MappingId);

					yield return List;
				}
			}
		}
	}
}
