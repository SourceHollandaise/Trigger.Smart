using Eto.Drawing;
using Eto.Forms;
using Trigger.Datastore.Persistent;
using Trigger.WinForms.Actions;

namespace Trigger.WinForms.Actions
{

	public class ModelModificationController : ActionBaseController
	{
		protected PersistentModelBase Model
		{
			get;
			set;
		}

		public ModelModificationController(Form form, PersistentModelBase model) : base(form)
		{
			this.Model = model;
		}

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

		public override System.Collections.Generic.IEnumerable<ToolItem> RegisterActions()
		{
			SaveAction = new ButtonToolItem();
			SaveAction.Text = "Save";
			SaveAction.ID = "Save_Tool_Action";
			SaveAction.Click += (sender, e) =>
			{
				var result = MessageBox.Show("Save " + Model.GetRepresentation() + "?", MessageBoxButtons.YesNo, MessageBoxType.Question, MessageBoxDefaultButton.No);
				if (result == DialogResult.Yes)
					SaveExecute();
			};

			yield return SaveAction;

			DeleteAction = new ButtonToolItem();
			DeleteAction.Text = "Delete";
			DeleteAction.ID = "Delete_Tool_Action";
			DeleteAction.Click += (sender, e) =>
			{
				var result = MessageBox.Show("Delete " + Model.GetRepresentation() + "?", MessageBoxButtons.YesNo, MessageBoxType.Warning, MessageBoxDefaultButton.No);
				if (result == DialogResult.Yes)
					DeleteExecute();
			};

			yield return DeleteAction;
		}

		protected virtual void SaveExecute()
		{
			Model.Save();
		}

		protected virtual void DeleteExecute()
		{
			Model.Delete();
		}
	}
}
