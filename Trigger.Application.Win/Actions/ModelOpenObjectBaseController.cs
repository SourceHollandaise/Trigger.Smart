using Eto.Forms;
using Trigger.Datastore.Persistent;
using Trigger.WinForms.Actions;
using System;
using System.Collections.Generic;
using Eto.Drawing;
using Trigger.WinForms.Layout;

namespace Trigger.WinForms.Actions
{
	public abstract class ModelOpenObjectBaseController : ActionBaseController
	{
		public ButtonToolItem OpenAction
		{
			get;
			protected set;
		}

		protected ModelOpenObjectBaseController(TemplateBase template, Type modelType, IPersistentId model) : base(template, model)
		{
			this.ModelType = modelType;
		}

		public override IEnumerable<ToolItem> ActionItems()
		{
			OpenAction = new ButtonToolItem();
			OpenAction.ID = "Open_Tool_Action";
			OpenAction.Image = Bitmap.FromResource("Edit32.png");
			OpenAction.Text = "Open";

			OpenAction.Click += (sender, e) =>
			{
				OpenObjectActionExecute();
			};

			yield return OpenAction;
		}

		public virtual void OpenObjectActionExecute()
		{

		}
	}
}
