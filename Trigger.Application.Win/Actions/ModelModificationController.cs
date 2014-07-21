using Eto.Forms;
using Trigger.Datastore.Persistent;
using Trigger.WinForms.Actions;
using System.Collections.Generic;
using Eto.Drawing;
using Trigger.WinForms.Layout;

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

		public ModelModificationController(TemplateBase template, IPersistentId model) : base(template, model)
		{

		}

		public override IEnumerable<ToolItem> ActionItems()
		{
			SaveAction = new ButtonToolItem();
			SaveAction.ID = "Save_Tool_Action";
			SaveAction.Image = Bitmap.FromResource("Save32.png");
			SaveAction.Text = "Save";
			SaveAction.Click += (sender, e) =>
			{
				var result = MessageBox.Show("Save " + CurrentObject.GetRepresentation() + "?", MessageBoxButtons.YesNo, MessageBoxType.Question, MessageBoxDefaultButton.No);
				if (result == DialogResult.Yes)
					SaveActionExecute();
			};

			yield return SaveAction;

			DeleteAction = new ButtonToolItem();
			DeleteAction.ID = "Delete_Tool_Action";
			DeleteAction.Image = Bitmap.FromResource("Delete32.png");
			DeleteAction.Text = "Delete";
			DeleteAction.Click += (sender, e) =>
			{
				var result = MessageBox.Show("Delete " + CurrentObject.GetRepresentation() + "?", MessageBoxButtons.YesNo, MessageBoxType.Warning, MessageBoxDefaultButton.No);
				if (result == DialogResult.Yes)
					DeleteActionExecute();
			};

			yield return DeleteAction;
		}

		public virtual void SaveActionExecute()
		{
			if (CurrentObject != null)
				CurrentObject.Save();
		}

		public virtual void DeleteActionExecute()
		{
			if (CurrentObject != null)
				CurrentObject.Delete();
		}
	}
}
