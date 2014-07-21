using Eto.Forms;
using Trigger.Datastore.Persistent;
using Trigger.WinForms.Actions;
using System;
using Trigger.WinForms.Layout;

namespace Trigger.WinForms.Actions
{
	public class ModelOpenObjectListController : ModelOpenObjectBaseController
	{
		public ModelOpenObjectListController(TemplateBase template, Type modelType, IPersistentId model) : base(template, modelType, model)
		{
			this.ModelType = modelType;
		}

		public override void OpenObjectActionExecute()
		{
			var listForm = Template as ListViewTemplate;

			if (listForm != null)
			{
				var selection = listForm.CurrentGrid.SelectedItem as IPersistentId;
				if (selection != null)
				{
					var detailTemplate = TemplateManager.GetDetailTemplate(selection);
					if (detailTemplate.Visible)
						detailTemplate.BringToFront();
					else
						detailTemplate.Show();
				}
			}
		}
	}
	
}
