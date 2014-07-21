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

		public IPersistent CurrentObject
		{
			get;
			set;
		}

		public Type ModelType
		{
			get;
			set;
		}

		protected TemplateBase(Type type, IPersistent currentObject)
		{
			this.ModelType = type;
			this.CurrentObject = currentObject;
			this.ID = this.Title;
		
			if (this.ToolBar == null)
				this.ToolBar = new ToolBar();

			this.ToolBar.TextAlign = ToolBarTextAlign.Right;

			if (this.Menu == null)
				this.Menu = new MenuBar();

			Controllers.Add(new ActionActiveWindowsController(this, CurrentObject));

			if (this as MainViewTemplate == null)
				Controllers.Add(new ActionCloseController(this, CurrentObject));
		}

		public override void OnLoadComplete(EventArgs e)
		{
			base.OnLoadComplete(e);

			LoadControllers(Controllers);

			if (this is DetailViewTemplate)
				WindowManager.AddDetailView(CurrentObject, this as DetailViewTemplate);

			if (this is ListViewTemplate)
				WindowManager.AddListView(ModelType, this as ListViewTemplate);
		}


		public override void OnClosed(EventArgs e)
		{
			base.OnClosed(e);

			if (this is ListViewTemplate)
				WindowManager.RemoveListView(ModelType);

			if (this is DetailViewTemplate)
				WindowManager.RemoveDetailView(CurrentObject);
		}

		public void LoadControllers(IEnumerable<ActionBaseController> controllers)
		{
			foreach (var controller in controllers)
			{
				foreach (var action in controller.ActionItems())
				{
					if (!this.ToolBar.Items.Contains(action))
					{
						if (string.IsNullOrWhiteSpace(action.ToolTip))
							action.ToolTip = action.Text;
						this.ToolBar.Items.Add(action);
					}
				}

				foreach (var action in controller.MenuItems())
				{
					if (!this.Menu.Items.Contains(action))
					if (string.IsNullOrWhiteSpace(action.ToolTip))
						action.ToolTip = action.Text;
					this.Menu.Items.Add(action);
				}
			}
		}

		public void UnloadController(ActionBaseController controller)
		{
			if (Controllers.Contains(controller))
			{
				foreach (var action in controller.ActionItems())
					this.ToolBar.Items.Remove(action);

				foreach (var action in controller.MenuItems())
					this.Menu.Items.Remove(action);

				Controllers.Remove(controller);
			}
		}

	}
}
