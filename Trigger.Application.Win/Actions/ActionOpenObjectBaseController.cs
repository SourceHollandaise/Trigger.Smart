using Eto.Forms;
using Trigger.Datastore.Persistent;
using Trigger.WinForms.Actions;
using System;
using System.Collections.Generic;
using Eto.Drawing;
using Trigger.WinForms.Layout;

namespace Trigger.WinForms.Actions
{
	public abstract class ActionOpenObjectBaseController : ActionBaseController
	{
		public Command OpenAction
		{
			get;
			protected set;
		}

		protected ActionOpenObjectBaseController(TemplateBase template, Type modelType, IPersistent model) : base(template, model)
		{
			this.ModelType = modelType;
		}

		public override IEnumerable<Command> Commands()
		{
			OpenAction = new Command();
			OpenAction.ID = "Open_Tool_Action";
			OpenAction.Image = ImageExtensions.GetImage("Edit32.png", 24);
			OpenAction.MenuText = "Open";

			OpenAction.Executed += (sender, e) =>
			{
				OpenObjectActionExecute();
			};

			yield return OpenAction;
		}

		public virtual void OpenObjectActionExecute()
		{

		}
	}
}
