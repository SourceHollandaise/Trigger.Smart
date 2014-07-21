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
		public Command RefreshAction
		{
			get;
			protected set;
		}

		protected ActionRefreshBaseController(TemplateBase template, Type modelType, IPersistent model) : base(template, model)
		{
			Category = "Edit";
		}

		public override IEnumerable<Command> Commands()
		{
			RefreshAction = new Command();
			RefreshAction.ID = "Refresh_Tool_Action";
			RefreshAction.Image = ImageExtensions.GetImage("Refresh32.png", 24);
			RefreshAction.MenuText = "Refresh";
			RefreshAction.ToolBarText = "Refresh";
			RefreshAction.Executed += (sender, e) =>
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
