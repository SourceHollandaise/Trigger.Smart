using System;
using System.Linq;
using Eto.Forms;
using Trigger.Datastore.Persistent;

namespace Trigger.CRM.Forms.Layout
{

	public class ModelListLayoutManager
	{
		readonly IStore store = Dependency.DependencyMapProvider.Instance.ResolveType<IStore>();

		public ListBox GetLayout(Type modelType)
		{
			ListBox list = new ListBox();

			var items = store.LoadAll(modelType).ToList();

			foreach (PersistentModelBase item in items)
			{
				var listItem = new ListItem();
				listItem.Key = item.MappingId.ToString();
				listItem.Tag = item;
				listItem.Text = item.GetRepresentation();
				list.Items.Add(listItem);
			}

			return 	list;
		}
	}
}
