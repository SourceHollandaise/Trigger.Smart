using Eto.Forms;
using Trigger.Datastore.Persistent;
using Trigger.WinForms.Actions;
using System.Collections.Generic;
using System;
using Eto.Drawing;
using Trigger.WinForms.Layout;

namespace Trigger.WinForms.Actions
{
	public abstract class ActionRefreshBaseController : ActionBaseController
	{
		public ButtonToolItem RefreshAction
		{
			get;
			protected set;
		}

		protected ActionRefreshBaseController(TemplateBase template, Type modelType, IPersistent model) : base(template, model)
		{
			this.ModelType = modelType;
		}

		public override IEnumerable<ToolItem> ActionItems()
		{
			RefreshAction = new ButtonToolItem();
			RefreshAction.ID = "Refresh_Tool_Action";
			RefreshAction.Image = ImageExtensions.GetImage("Refresh32.png", 24);
			RefreshAction.Text = "Refresh";

			RefreshAction.Click += (sender, e) =>
			{
				RefreshActionExecute();
			};

			yield return RefreshAction;
		}

		public virtual void RefreshActionExecute()
		{


		}
	}
}
