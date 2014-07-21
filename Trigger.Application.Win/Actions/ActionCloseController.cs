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
		public ButtonToolItem CloseAction
		{
			get;
			protected set;
		}

		public ActionCloseController(TemplateBase template, IPersistent model) : base(template, model)
		{

		}

		public override IEnumerable<ToolItem> ActionItems()
		{
			CloseAction = new ButtonToolItem();
			CloseAction.ID = "Close_Tool_Action";
			CloseAction.Image = ImageExtensions.GetImage("Close32.png", 24);
			CloseAction.Text = "Close";

			CloseAction.Click += (sender, e) =>
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
