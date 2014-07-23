using Trigger.Datastore.Persistent;
using Trigger.WinForms.Actions;
using System;
using Trigger.WinForms.Layout;

namespace Trigger.WinForms.Actions
{
	public class ActionOpenObjectListController : ActionOpenObjectBaseController
	{
		public ActionOpenObjectListController(TemplateBase template, Type modelType, IStorable currentObject) : base(template, modelType, currentObject)
		{
			this.ModelType = modelType;
			TargetView = ActionControllerTargetView.ListView;
		}

		public override void OpenObjectActionExecute()
		{
			var listForm = Template as ListViewTemplate;

			if (listForm != null)
			{
				WindowManager.ShowDetailView(listForm.CurrentGrid.SelectedItem as IStorable);
			}
		}
	}
}
