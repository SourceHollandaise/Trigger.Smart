using Eto.Forms;
using Trigger.Datastore.Persistent;
using Trigger.WinForms.Actions;
using System;
using Trigger.WinForms.Layout;
using System.Collections.Generic;
using Eto.Drawing;

namespace Trigger.WinForms.Actions
{
	public class ModelNewObjectActionController : ActionBaseController
	{
		public ButtonToolItem NewAction
		{
			get;
			protected set;
		}

		public ModelNewObjectActionController(Form template, Type modelType, IPersistentId model) : base(template, model)
		{
			this.ModelType = modelType;
		}

		public override IEnumerable<ToolItem> RegisterActions()
		{
			NewAction = new ButtonToolItem();
			NewAction.ID = "New_Tool_Action";
			NewAction.Image = Bitmap.FromResource("Add32.png");
			NewAction.Text = "New " + ModelType.Name;

			NewAction.Click += (sender, e) =>
			{
				NewObjectExecute();
			};

			yield return NewAction;
		}

		protected virtual void NewObjectExecute()
		{
			CurrentObject = Activator.CreateInstance(ModelType) as IPersistentId;
			CurrentObject.Initialize();
	
			var detailForm = new ModelDetailForm(ModelType, CurrentObject);
			detailForm.Show();
		}
	}
	
}
