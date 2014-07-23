using Trigger.Datastore.Persistent;
using Trigger.WinForms.Actions;
using System;
using Trigger.WinForms.Layout;

namespace Trigger.WinForms.Actions
{

	public class ActionRefreshDetailController : ActionRefreshBaseController
	{
		public ActionRefreshDetailController(TemplateBase template, Type modelType, IStorable currentObject) : base(template, modelType, currentObject)
		{
			this.ModelType = modelType;
			TargetView = ActionControllerTargetView.DetailView;
		}

		public override void RefreshActionExecute()
		{
			var detailForm = Template as DetailViewTemplate;
			if (detailForm != null)
				detailForm.ReloadObject();
		}
	}
}
