using Eto.Drawing;
using Eto.Forms;
using Trigger.Datastore.Persistent;
using Trigger.WinForms.Actions;
using System;
using Trigger.WinForms.Layout;
using System.Collections.Generic;

namespace Trigger.WinForms.Actions
{
	public class ModelNewObjectActionController : ActionBaseController
	{
		public ButtonToolItem NewAction
		{
			get;
			protected set;
		}

		public ModelNewObjectActionController(Form template, Type modelType, PersistentModelBase model) : base(template, model)
		{
			this.ModelType = modelType;
		}

		public override IEnumerable<ToolItem> RegisterActions()
		{
			NewAction = new ButtonToolItem();
			NewAction.Text = "New " + ModelType.Name;
			NewAction.ID = "New_Tool_Action";

			NewAction.Click += (sender, e) =>
			{
				NewObjectExecute();
			};

			yield return NewAction;
		}

		protected virtual void NewObjectExecute()
		{
			CurrentObject = Activator.CreateInstance(ModelType) as PersistentModelBase;

			var detailTemplate = new ModelDetailForm(ModelType, CurrentObject);
			detailTemplate.Show();
		}
	}
	
}
