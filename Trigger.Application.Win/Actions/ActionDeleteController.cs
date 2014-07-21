using Eto.Forms;
using Trigger.Datastore.Persistent;
using Trigger.WinForms.Actions;
using System.Collections.Generic;
using Eto.Drawing;
using Trigger.WinForms.Layout;

namespace Trigger.WinForms.Actions
{
	public class ActionDeleteController : ActionBaseController
	{
		public Command DeleteAction
		{
			get;
			protected set;
		}

		public ActionDeleteController(TemplateBase template, IPersistent model) : base(template, model)
		{
			Category = "Edit";
		}

		public override IEnumerable<Command> Commands()
		{
			DeleteAction = new Command();
			DeleteAction.ID = "Delete_Tool_Action";
			DeleteAction.Image = ImageExtensions.GetImage("Delete32.png", 24);
			DeleteAction.MenuText = "Delete";
			DeleteAction.ToolBarText = "Delete";
			DeleteAction.Executed += (sender, e) =>
			{
				var result = MessageBox.Show("Delete " + CurrentObject.GetRepresentation() + "?", MessageBoxButtons.YesNo, MessageBoxType.Warning, MessageBoxDefaultButton.No);
				if (result == DialogResult.Yes)
					DeleteActionExecute();
			};

			yield return DeleteAction;
		}

		public virtual void DeleteActionExecute()
		{
			if (CurrentObject != null)
				CurrentObject.Delete();
		}
	}
}