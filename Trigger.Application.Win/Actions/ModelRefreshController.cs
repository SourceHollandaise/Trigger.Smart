using Eto.Forms;
using Trigger.Datastore.Persistent;
using Trigger.WinForms.Actions;
using System.Collections.Generic;
using System;

namespace Trigger.WinForms.Actions
{
	public abstract class ModelRefreshController : ActionBaseController
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


		}
	}
}
