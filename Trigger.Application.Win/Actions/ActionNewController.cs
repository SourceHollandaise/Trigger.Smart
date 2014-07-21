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
		public ButtonToolItem NewAction
		{
			get;
			protected set;
		}

		public ActionNewController(TemplateBase template, Type modelType, IPersistent model) : base(template, model)
		{
			this.ModelType = modelType;
		}

		public override IEnumerable<ToolItem> ActionItems()
		{
			NewAction = new ButtonToolItem();
			NewAction.ID = "New_Tool_Action";
			NewAction.Image = ImageExtensions.GetImage("Add32.png", 24);
			NewAction.Text = "New " + ModelType.Name;

			NewAction.Click += (sender, e) =>
			{
				NewActionExecute();
			};

			yield return NewAction;
		}

		public virtual void NewActionExecute()
		{
			CurrentObject = Activator.CreateInstance(ModelType) as IPersistent;
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
