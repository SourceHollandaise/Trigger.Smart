using Eto.Forms;
using Trigger.XStorable.DataStore;
using Trigger.XForms.Controllers;
using System.Linq;
using System;
using Trigger.XForms.Visuals;
using Trigger.XStorable.Dependency;

namespace Trigger.XForms
{
	public static class ListViewTemplateExtensions
	{
		public static void ReloadList(this ListViewTemplate listForm)
		{
			if (listForm != null)
			{
				var store = DependencyMapProvider.Instance.ResolveType<IStore>();

				listForm.CurrentGrid.DataStore = new DataStoreCollection(store.LoadAll(listForm.ModelType));

				listForm.Title = listForm.ModelType.Name + "-List - Items: " + listForm.CurrentGrid.DataStore.AsEnumerable().Count();
			}
		}
	}
}
