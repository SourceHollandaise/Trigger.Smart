using Eto.Forms;
using Trigger.Datastore.Persistent;
using Trigger.WinForms.Actions;
using System.Collections.Generic;
using Eto.Drawing;
using Trigger.WinForms.Layout;
using System;

namespace Trigger.WinForms.Actions
{
	public class ActionSaveController : ActionBaseController
	{
		public Command SaveAction
		{
			get;
			protected set;
		}

		public ActionSaveController(TemplateBase template, Type modelType, IStorable currentObject) : base(template, modelType, currentObject)
		{
			TargetView = ActionControllerTargetView.DetailView;
		}

		public override IEnumerable<Command> Commands()
		{
			SaveAction = new Command();
			SaveAction.ID = "Save_Tool_Action";
			SaveAction.Image = ImageExtensions.GetImage("Save32.png", 24);
			SaveAction.MenuText = "Save";
			SaveAction.ToolBarText = "Save";
			SaveAction.Shortcut = Keys.Control & Keys.S;
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
