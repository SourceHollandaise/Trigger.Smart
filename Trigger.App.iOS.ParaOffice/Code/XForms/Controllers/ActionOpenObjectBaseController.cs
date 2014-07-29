using Eto.Forms;
using Trigger.XStorable.DataStore;
using System;
using System.Collections.Generic;
using Eto.Drawing;
using Trigger.XForms.Visuals;

namespace Trigger.XForms.Controllers
{
	public abstract class ActionOpenObjectBaseController : ActionBaseController
	{
		public Command OpenAction
		{
			get;
			protected set;
		}

		protected ActionOpenObjectBaseController(TemplateBase template, Type modelType, IStorable currentObject) : base(template, modelType, currentObject)
		{
			this.ModelType = modelType;
		}

		public override IEnumerable<Command> Commands()
		{
			OpenAction = new Command();
			OpenAction.ID = "Open_Tool_Action";
			OpenAction.Image = ImageExtensions.GetImage("Edit32.png", 32);
			OpenAction.MenuText = "Open";
			OpenAction.ToolBarText = "Open";
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
