using Eto.Forms;
using Trigger.Datastore.Persistent;
using Trigger.WinForms.Actions;
using System;
using Trigger.WinForms.Layout;

namespace Trigger.WinForms.Actions
{
	public class ModelOpenObjectListController : ModelOpenObjectBaseController
	{
		public ModelOpenObjectListController(Form template, Type modelType, IPersistentId model) : base(template, modelType, model)
		{
			this.ModelType = modelType;
		}

		protected override void OpenObjectExecute()
		{
			var listForm = Template as ModelListForm;
			if (listForm != null)
			{
				var selection = listForm.CurrentGrid.SelectedItem as IPersistentId;
				if (selection != null)
				{
					var detailForm = new ModelDetailForm(ModelType, selection);
					detailForm.Show();
				}
			}
		}
	}
	
}
