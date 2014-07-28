using Trigger.XStorable.DataStore;
using System;
using Trigger.XForms.Visuals;

namespace Trigger.XForms.Controllers
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
