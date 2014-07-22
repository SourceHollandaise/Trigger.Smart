using Trigger.Datastore.Persistent;
using Trigger.WinForms.Actions;
using System;
using Trigger.WinForms.Layout;

namespace Trigger.WinForms.Actions
{
	public class ActionRefreshListController : ActionRefreshBaseController
	{
		public ActionRefreshListController(TemplateBase template, Type modelType, IPersistent model) : base(template, modelType, model)
		{
			this.ModelType = modelType;
			TargetView = ActionControllerTargetView.ListView;
		}

		public override void RefreshActionExecute()
		{
			var listForm = Template as ListViewTemplate;
			if (listForm != null)
				listForm.ReloadList();
		}
	}
}
