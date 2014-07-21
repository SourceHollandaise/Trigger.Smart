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
		public ButtonToolItem DeleteAction
		{
			get;
			protected set;
		}

		public ActionDeleteController(TemplateBase template, IPersistent model) : base(template, model)
		{

		}

		public override IEnumerable<ToolItem> ActionItems()
		{
			DeleteAction = new ButtonToolItem();
			DeleteAction.ID = "Delete_Tool_Action";
			DeleteAction.Image = ImageExtensions.GetImage("Delete32.png", 24);
			DeleteAction.Text = "Delete";
			DeleteAction.Click += (sender, e) =>
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
