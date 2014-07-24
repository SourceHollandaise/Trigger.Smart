using Eto.Forms;
using Trigger.XStorable.DataStore;
using System.Collections.Generic;
using System;
using Eto.Drawing;
using Trigger.XForms.Visuals;

namespace Trigger.XForms.Controllers
{
	public abstract class ActionRefreshBaseController : ActionBaseController
	{
		public Command RefreshAction
		{
			get;
			protected set;
		}

		protected ActionRefreshBaseController(TemplateBase template, Type modelType, IStorable currentObject) : base(template, modelType, currentObject)
		{
			Category = "Edit";
		}

		public override IEnumerable<Command> Commands()
		{
			RefreshAction = new Command();
			RefreshAction.ID = "Refresh_Tool_Action";
			RefreshAction.Image = ImageExtensions.GetImage("Refresh32.png", 32);
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
