using Eto.Forms;
using Trigger.Datastore.Persistent;
using Trigger.WinForms.Actions;
using System;
using Trigger.WinForms.Layout;
using System.Linq;

namespace Trigger.WinForms.Actions
{
	public class ModelRefreshListController : ModelRefreshBaseController
	{
		public ModelRefreshListController(Form template, Type modelType, IPersistentId model) : base(template, modelType, model)
		{
			this.ModelType = modelType;
		}

		protected override void RefreshExecute()
		{
			var listForm = Template as ModelListForm;
			if (listForm != null)
			{
				var store = Dependency.DependencyMapProvider.Instance.ResolveType<IStore>();

				listForm.CurrentGrid.DataStore = new DataStoreCollection(store.LoadAll(ModelType));
				listForm.Title = ModelType.Name + "-List - Items: " + listForm.CurrentGrid.DataStore.AsEnumerable().Count();
			}
		}
	}

}
