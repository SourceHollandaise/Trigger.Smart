using Eto.Forms;
using Trigger.Datastore.Persistent;
using Trigger.WinForms.Actions;
using System.Collections.Generic;
using System;
using Eto.Drawing;

namespace Trigger.WinForms.Actions
{
	public abstract class ModelRefreshBaseController : ActionBaseController
	{
		public ButtonToolItem RefreshAction
		{
			get;
			protected set;
		}

		protected ModelRefreshBaseController(Form template, Type modelType, IPersistentId model) : base(template, model)
		{
			this.ModelType = modelType;
		}

		public override IEnumerable<ToolItem> RegisterActions()
		{
			RefreshAction = new ButtonToolItem();
			RefreshAction.ID = "Refresh_Tool_Action";
			RefreshAction.Image = Bitmap.FromResource("Refresh32.png");
			RefreshAction.Text = "Refresh";

			RefreshAction.Click += (sender, e) =>
			{
				RefreshExecute();
			};

			yield return RefreshAction;
		}

		protected virtual void RefreshExecute()
		{


		}
	}
}
