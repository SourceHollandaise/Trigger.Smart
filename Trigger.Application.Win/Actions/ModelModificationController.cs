using Eto.Forms;
using Trigger.Datastore.Persistent;
using Trigger.WinForms.Actions;
using System.Collections.Generic;

namespace Trigger.WinForms.Actions
{
	public class ModelModificationController : ActionBaseController
	{
		public ButtonToolItem SaveAction
		{
			get;
			protected set;
		}

		public ButtonToolItem DeleteAction
		{
			get;
			protected set;
		}

		public ModelModificationController(Form template, PersistentModelBase model) : base(template, model)
		{

		}

		public override IEnumerable<ToolItem> RegisterActions()
		{
			SaveAction = new ButtonToolItem();
			SaveAction.Text = "Save";
			SaveAction.ID = "Save_Tool_Action";
			SaveAction.Click += (sender, e) =>
			{
				var result = MessageBox.Show("Save " + CurrentObject.GetRepresentation() + "?", MessageBoxButtons.YesNo, MessageBoxType.Question, MessageBoxDefaultButton.No);
				if (result == DialogResult.Yes)
					SaveExecute();
			};

			yield return SaveAction;

			DeleteAction = new ButtonToolItem();
			DeleteAction.Text = "Delete";
			DeleteAction.ID = "Delete_Tool_Action";
			DeleteAction.Click += (sender, e) =>
			{
				var result = MessageBox.Show("Delete " + CurrentObject.GetRepresentation() + "?", MessageBoxButtons.YesNo, MessageBoxType.Warning, MessageBoxDefaultButton.No);
				if (result == DialogResult.Yes)
					DeleteExecute();
			};

			yield return DeleteAction;
		}

		protected virtual void SaveExecute()
		{
			CurrentObject.Save();
		}

		protected virtual void DeleteExecute()
		{
			CurrentObject.Delete();
		}
	}
}
