using Eto.Forms;
using Trigger.Datastore.Persistent;
using Trigger.WinForms.Actions;
using System;
using Trigger.WinForms.Layout;
using System.Collections.Generic;
using Eto.Drawing;

namespace Trigger.WinForms.Actions
{
	public class ActionCloseController : ActionBaseController
	{
		public Command CloseAction
		{
			get;
			protected set;
		}

		public ActionCloseController(TemplateBase template, Type modelType, IPersistent currentObject) : base(template, modelType, currentObject)
		{
			Category = "File";
			TargetView = ActionControllerTargetView.Any;
		}

		public override IEnumerable<Command> Commands()
		{
			CloseAction = new Command();
			CloseAction.ID = "Close_Tool_Action";
			CloseAction.Image = ImageExtensions.GetImage("Close32.png", 24);
			CloseAction.MenuText = "Close";
			CloseAction.ToolBarText = "Close";
			CloseAction.Executed += (sender, e) =>
			{
				CloseActionExecute();
			};

			yield return CloseAction;
		}

		protected virtual void CloseActionExecute()
		{
			Template.Close();
		}
	}
}
