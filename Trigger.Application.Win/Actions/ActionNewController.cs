using Eto.Forms;
using Trigger.Datastore.Persistent;
using Trigger.WinForms.Actions;
using System;
using Trigger.WinForms.Layout;
using System.Collections.Generic;
using Eto.Drawing;

namespace Trigger.WinForms.Actions
{

	public class ActionNewController : ActionBaseController
	{
		public Command NewAction
		{
			get;
			protected set;
		}

		public ActionNewController(TemplateBase template, Type modelType, IStorable currentObject) : base(template, modelType, currentObject)
		{
			this.ModelType = modelType;
			TargetView = ActionControllerTargetView.ListView;
		}

		public override IEnumerable<Command> Commands()
		{
			NewAction = new Command();
			NewAction.ID = "New_Tool_Action";
			NewAction.Image = ImageExtensions.GetImage("Add32.png", 24);
			NewAction.MenuText = "New " + ModelType.Name;
			NewAction.ToolBarText = "New " + ModelType.Name;
			NewAction.Executed += (sender, e) =>
			{
				NewActionExecute();
			};

			yield return NewAction;
		}

		public virtual void NewActionExecute()
		{
			CurrentObject = Activator.CreateInstance(ModelType) as IStorable;
			CurrentObject.Initialize();
	
			var detailForm = new DetailViewTemplate(ModelType, CurrentObject);
			detailForm.Show();

			detailForm.Closed += (sender, e) =>
			{
				var listForm = Template as ListViewTemplate;
				if (listForm != null)
					listForm.ReloadList();
			};
		}
	}
	
}
