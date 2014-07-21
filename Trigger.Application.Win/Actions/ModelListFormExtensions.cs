using Eto.Forms;
using Trigger.Datastore.Persistent;
using Trigger.WinForms.Layout;
using System.Linq;

namespace Trigger.WinForms.Actions
{

	public static class ModelListFormExtensions
	{
		public static void ReloadList(this ListViewTemplate listForm)
		{
			if (listForm != null)
			{
				var store = Dependency.DependencyMapProvider.Instance.ResolveType<IStore>();

				listForm.CurrentGrid.DataStore = new DataStoreCollection(store.LoadAll(listForm.ModelType));
				listForm.Title = listForm.ModelType.Name + "-List - Items: " + listForm.CurrentGrid.DataStore.AsEnumerable().Count();
			}
		}
	}
}
