using Trigger.Datastore.Persistent;
using Trigger.WinForms.Actions;
using System;
using Trigger.WinForms.Layout;

namespace Trigger.WinForms.Actions
{
	public class ActionOpenObjectListController : ActionOpenObjectBaseController
	{
		public ActionOpenObjectListController(TemplateBase template, Type modelType, IPersistent model) : base(template, modelType, model)
		{
			this.ModelType = modelType;
		}

		public override void OpenObjectActionExecute()
		{
			var listForm = Template as ListViewTemplate;

			if (listForm != null)
			{
				WindowManager.ShowDetailView(listForm.CurrentGrid.SelectedItem as IPersistent);
			}
		}
	}
}