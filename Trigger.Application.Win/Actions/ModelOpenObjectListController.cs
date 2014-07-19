using Eto.Forms;
using Trigger.Datastore.Persistent;
using Trigger.WinForms.Actions;
using System;
using Trigger.WinForms.Layout;

namespace Trigger.WinForms.Actions
{

	public class ModelOpenObjectListController : ModelOpenObjectBaseController
	{
		public ModelOpenObjectListController(Form template, Type modelType, PersistentModelBase model) : base(template, modelType, model)
		{
			this.ModelType = modelType;
		}

		protected override void OpenObjectExecute()
		{
			var modelListForm = Template as ModelListForm;
			if (modelListForm != null)
			{
				var selection = modelListForm.CurrentGrid.SelectedItem as PersistentModelBase;
				if (selection != null)
				{
					var detailTemplate = new ModelDetailForm(ModelType, selection);
					detailTemplate.Show();
				}
			}
		}
	}
	
}
