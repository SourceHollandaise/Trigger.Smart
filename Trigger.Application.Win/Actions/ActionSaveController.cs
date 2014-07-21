using Eto.Forms;
using Trigger.Datastore.Persistent;
using Trigger.WinForms.Actions;
using System.Collections.Generic;
using Eto.Drawing;
using Trigger.WinForms.Layout;

namespace Trigger.WinForms.Actions
{
	public class ActionSaveController : ActionBaseController
	{
		public Command SaveAction
		{
			get;
			protected set;
		}

		public ActionSaveController(TemplateBase template, IPersistent model) : base(template, model)
		{

		}

		public override IEnumerable<Command> Commands()
		{
			SaveAction = new Command();
			SaveAction.ID = "Save_Tool_Action";
			SaveAction.Image = ImageExtensions.GetImage("Save32.png", 24);
			SaveAction.MenuText = "Save";
			SaveAction.ToolBarText = "Save";
			SaveAction.Executed += (sender, e) =>
			{
				var result = MessageBox.Show("Save " + CurrentObject.GetRepresentation() + "?", MessageBoxButtons.YesNo, MessageBoxType.Question, MessageBoxDefaultButton.No);
				if (result == DialogResult.Yes)
					SaveActionExecute();
			};

			yield return SaveAction;
		}

		public virtual void SaveActionExecute()
		{
			if (CurrentObject != null)
				CurrentObject.Save();
		}
	}
}
