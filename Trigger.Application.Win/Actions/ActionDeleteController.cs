using Eto.Forms;
using Trigger.Datastore.Persistent;
using Trigger.WinForms.Actions;
using System.Collections.Generic;
using Eto.Drawing;
using Trigger.WinForms.Layout;
using System;

namespace Trigger.WinForms.Actions
{
	public class ActionDeleteController : ActionBaseController
	{
		public Command DeleteAction
		{
			get;
			protected set;
		}

		public ActionDeleteController(TemplateBase template, Type modelType, IStorable currentObject) : base(template, modelType, currentObject)
		{
			Category = "Edit";
			TargetView = ActionControllerTargetView.DetailView;
		}

		public override IEnumerable<Command> Commands()
		{
			DeleteAction = new Command();
			DeleteAction.ID = "Delete_Tool_Action";
			DeleteAction.Image = ImageExtensions.GetImage("Delete32.png", 24);
			DeleteAction.MenuText = "Delete";
			DeleteAction.ToolBarText = "Delete";
			DeleteAction.Shortcut = Keys.Control & Keys.D;
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

			var listForm = WindowManager.GetListView(ModelType);
			if (listForm != null)
				listForm.ReloadList();
		}
	}
}
