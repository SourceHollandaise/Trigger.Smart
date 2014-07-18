using System;
using Eto.Forms;
using Trigger.Datastore.Persistent;

namespace Trigger.CRM.Forms
{

	public class SaveButtonAction : ButtonAction
	{
		public const string ActionID = "save_action";

		PersistentModelBase model;

		public SaveButtonAction(PersistentModelBase model)
		{
			this.model = model;
			this.ID = ActionID;
			this.MenuText = "Save";
			this.ToolBarText = "Save item";
			this.TooltipText = "Saves the current item to store";
			//this.Icon = Icon.FromResource ("MyResourceName.ico");
			this.Accelerator = Application.Instance.CommonModifier | Keys.Enter;  // control+M or cmd+M
		}

		protected override void OnActivated(EventArgs e)
		{
			base.OnActivated(e);

			model.Save();
		}
	}
}
