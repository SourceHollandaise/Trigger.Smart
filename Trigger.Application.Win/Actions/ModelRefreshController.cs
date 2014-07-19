using Eto.Forms;
using Trigger.Datastore.Persistent;
using Trigger.WinForms.Actions;
using System.Collections.Generic;
using System;
using Trigger.WinForms.Layout;

namespace Trigger.WinForms.Actions
{
	public class ModelRefreshController : ActionBaseController
	{
		public ButtonToolItem ResfreshAction
		{
			get;
			protected set;
		}

		public ModelRefreshController(Form template, Type modelType, PersistentModelBase model) : base(template, model)
		{
			this.ModelType = modelType;
		}

		public override IEnumerable<ToolItem> RegisterActions()
		{
			ResfreshAction = new ButtonToolItem();
			ResfreshAction.Text = "Refresh";
			ResfreshAction.ID = "Refresh_Tool_Action";
			ResfreshAction.Click += (sender, e) =>
			{
				RefreshExecute();
			};

			yield return ResfreshAction;
		}

		protected virtual void RefreshExecute()
		{
			var listTemplate = Template as ModelListForm;
			if (listTemplate != null)
			{
				var layout = new ModelListLayoutManager().GetLayout(ModelType);

				listTemplate.CurrentList.Items.Clear();
				listTemplate.CurrentList.Items.AddRange(layout.Items);
			}
			var detailTemplate = Template as ModelDetailForm;
			if (detailTemplate != null)
			{
				var layout = new ModelDetailLayoutManager().GetLayout(CurrentObject);
				detailTemplate.Content = layout;
				detailTemplate.ResumeLayout();
			}
		}
	}
}
