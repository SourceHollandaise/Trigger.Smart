using Eto.Forms;
using Trigger.Datastore.Persistent;
using Trigger.WinForms.Actions;
using System;
using Trigger.WinForms.Layout;

namespace Trigger.WinForms.Actions
{
	public class ModelRefreshListController : ModelRefreshController
	{
		public ModelRefreshListController(Form template, Type modelType, PersistentModelBase model) : base(template, modelType, model)
		{
			this.ModelType = modelType;
		}

		protected override void RefreshExecute()
		{
			var modelListForm = Template as ModelListForm;
			if (modelListForm != null)
			{
				var store = Dependency.DependencyMapProvider.Instance.ResolveType<IStore>();

				modelListForm.CurrentGrid.DataStore = new DataStoreCollection(store.LoadAll(ModelType));
			}
		}
	}
}
