using Trigger.XStorable.DataStore;
using System;
using Trigger.XForms.Visuals;

namespace Trigger.XForms.Controllers
{
	public class ActionRefreshListController : ActionRefreshBaseController
	{
		public ActionRefreshListController(TemplateBase template, Type modelType, IStorable currentObject) : base(template, modelType, currentObject)
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
