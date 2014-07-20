using Eto.Forms;
using Trigger.Datastore.Persistent;
using Trigger.WinForms.Actions;
using System;
using System.Collections.Generic;
using Eto.Drawing;

namespace Trigger.WinForms.Actions
{
	public abstract class ModelOpenObjectBaseController : ActionBaseController
	{
		public ButtonToolItem OpenAction
		{
			get;
			protected set;
		}

		protected ModelOpenObjectBaseController(Form template, Type modelType, IPersistentId model) : base(template, model)
		{
			this.ModelType = modelType;
		}

		public override IEnumerable<ToolItem> RegisterActions()
		{
			OpenAction = new ButtonToolItem();
			OpenAction.ID = "Open_Tool_Action";
			OpenAction.Image = Bitmap.FromResource("Edit32.png");
			OpenAction.Text = "Open";

			OpenAction.Click += (sender, e) =>
			{
				OpenObjectExecute();
			};

			yield return OpenAction;
		}

		protected virtual void OpenObjectExecute()
		{

		}
	}
}
