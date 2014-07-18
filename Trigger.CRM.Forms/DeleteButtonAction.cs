using System;
using Eto.Forms;
using Trigger.Datastore.Persistent;

namespace Trigger.CRM.Forms
{

	public class DeleteButtonAction : ButtonAction
	{
		public const string ActionID = "delete_action";

		PersistentModelBase model;

		public DeleteButtonAction(PersistentModelBase model)
		{
			this.model = model;
			this.ID = ActionID;
			this.MenuText = "Delete";
			this.ToolBarText = "Delete item";
			this.TooltipText = "Deletes the current item from store";
			//this.Icon = Icon.FromResource ("MyResourceName.ico");
			this.Accelerator = Application.Instance.CommonModifier | Keys.Delete;  // control+M or cmd+M
		}

		protected override void OnActivated(EventArgs e)
		{
			base.OnActivated(e);

			MessageBox.Show(Application.Instance.MainForm, "You clicked Delete!", "Del", MessageBoxButtons.OK);
		}
	}
	
}
