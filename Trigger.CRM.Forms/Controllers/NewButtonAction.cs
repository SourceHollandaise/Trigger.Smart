using System;
using Eto.Forms;
using Trigger.Datastore.Persistent;
using Trigger.CRM.Forms.Templates;

namespace Trigger.CRM.Forms
{

	public class NewButtonAction : ButtonAction
	{
		public const string ActionID = "new_action";

		Type modelType;

		public NewButtonAction(Type modelType)
		{
			this.modelType = modelType;
			this.ID = ActionID;
			this.MenuText = "New";
			this.ToolBarText = "New item";
			this.TooltipText = "Create a new entry";
			//this.Icon = Icon.FromResource ("MyResourceName.ico");
			this.Accelerator = Application.Instance.CommonModifier | Keys.N;  // control+M or cmd+M
		}

		protected override void OnActivated(EventArgs e)
		{
			base.OnActivated(e);

			var item = Activator.CreateInstance(modelType) as PersistentModelBase;

			var detailTemplate = new ModelDetailTemplate(item);
			detailTemplate.Show();
		}
	}
	
}
