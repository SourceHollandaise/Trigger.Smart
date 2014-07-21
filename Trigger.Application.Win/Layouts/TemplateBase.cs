using Eto.Forms;
using Trigger.Datastore.Persistent;
using System;
using System.Collections.Generic;
using Trigger.WinForms.Actions;

namespace Trigger.WinForms.Layout
{

	public abstract class TemplateBase : Form
	{
		public IList<ActionBaseController> Controllers = new List<ActionBaseController>();

		public IPersistentId CurrentObject
		{
			get;
			set;
		}

		public Type ModelType
		{
			get;
			set;
		}

		protected TemplateBase(Type type, IPersistentId currentObject)
		{
			this.ModelType = type;
			this.CurrentObject = currentObject;
		
			if (this.ToolBar == null)
				this.ToolBar = new ToolBar();
				
		}

		public override void OnLoadComplete(EventArgs e)
		{
			base.OnLoadComplete(e);

			LoadControllers(Controllers);
		}

		public void LoadControllers(IEnumerable<ActionBaseController> controllers)
		{
			foreach (var controller in controllers)
			{
				foreach (var action in controller.ActionItems())
					if (!this.ToolBar.Items.Contains(action))
						this.ToolBar.Items.Add(action);
			}
		}

		public void UnloadController(ActionBaseController controller)
		{
			if (Controllers.Contains(controller))
			{
				foreach (var action in controller.ActionItems())
					this.ToolBar.Items.Remove(action);

				Controllers.Remove(controller);
			}
		}
	}
}
