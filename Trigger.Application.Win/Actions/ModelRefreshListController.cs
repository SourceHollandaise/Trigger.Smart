using Eto.Forms;
using Trigger.Datastore.Persistent;
using Trigger.WinForms.Actions;
using System.Collections.Generic;
using System;
using Trigger.WinForms.Layout;

namespace Trigger.WinForms.Actions
{
	public class ModelRefreshListController : ModelRefreshController
	{
		public ModelRefreshListController(Form template, Type modelType, PersistentModelBase model) : base(template, modelType, model)
		{
			this.ModelType = modelType;
		}

		protected override void RefreshExecute()
		{
			var modelListForm = Template as ModelListForm;
			if (modelListForm != null)
			{
				if (modelListForm != null)
				{
					var layout = new ModelListLayoutManager().GetLayout(ModelType);

					modelListForm.CurrentList.Items.Clear();
					modelListForm.CurrentList.Items.AddRange(layout.Items);
				}
			}
		}
	}
	
}
