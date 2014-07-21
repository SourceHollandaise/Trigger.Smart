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

			Application.Instance.CreateStandardMenu(Menu.Items);

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
				var currentMenu = Menu.Items.GetSubmenu(controller.Category);

				switch (controller.Visiblity)
				{
					case ActionVisibility.MenuAndToolbar:
						currentMenu.Items.AddRange(controller.Commands());
						this.ToolBar.Items.AddRange(controller.Commands());
						break;
					
					case ActionVisibility.Menu:
						currentMenu.Items.AddRange(controller.Commands());
						break;
					
					case ActionVisibility.Toolbar:
						this.ToolBar.Items.AddRange(controller.Commands());
						break;
					
					case ActionVisibility.None:
						break;
					
					default:
						currentMenu.Items.AddRange(controller.Commands());
						this.ToolBar.Items.AddRange(controller.Commands());
						break;
				}

				this.Menu.Items.Trim();
			}
		}

		public void UnloadController(ActionBaseController controller)
		{
			if (Controllers.Contains(controller))
			{
				Controllers.Remove(controller);
			}
		}
	}
}