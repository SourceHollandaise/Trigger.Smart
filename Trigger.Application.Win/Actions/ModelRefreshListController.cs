using Trigger.Datastore.Persistent;
using Trigger.WinForms.Actions;
using System;
using Trigger.WinForms.Layout;

namespace Trigger.WinForms.Actions
{
	public class ModelRefreshListController : ModelRefreshBaseController
	{
		public ModelRefreshListController(TemplateBase template, Type modelType, IPersistentId model) : base(template, modelType, model)
		{
			this.ModelType = modelType;
		}

		public override void RefreshActionExecute()
		{
			var listForm = Template as ListViewTemplate;
			if (listForm != null)
				listForm.ReloadList();
		}
	}
}
