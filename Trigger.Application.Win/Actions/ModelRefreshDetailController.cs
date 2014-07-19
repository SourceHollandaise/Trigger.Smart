using Eto.Forms;
using Trigger.Datastore.Persistent;
using Trigger.WinForms.Actions;
using System;
using Trigger.WinForms.Layout;

namespace Trigger.WinForms.Actions
{

	public class ModelRefreshDetailController : ModelRefreshController
	{
		public ModelRefreshDetailController(Form template, Type modelType, IPersistentId model) : base(template, modelType, model)
		{
			this.ModelType = modelType;
		}

		protected override void RefreshExecute()
		{
			var detailForm = Template as ModelDetailForm;
			if (detailForm != null)
			{
				var store = Dependency.DependencyMapProvider.Instance.ResolveType<IStore>();
				var dataObject = store.Load(ModelType, CurrentObject.MappingId) as IPersistentId;
				if (dataObject != null)
				{
					var layout = new ModelDetailLayoutManager(dataObject).GetLayout();
					detailForm.Content = layout;
				}
			}
		}
	}
}
