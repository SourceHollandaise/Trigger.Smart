using Eto.Forms;
using Trigger.Datastore.Persistent;
using System;
using System.Collections.Generic;
using Trigger.WinForms.Actions;
using System.Linq;

namespace Trigger.WinForms.Layout
{
	public abstract class TemplateBase : Form
	{
		public IList<ActionBaseController> Controllers = new List<ActionBaseController>();

		public IPersistent CurrentObject
		{
			get;
			private set;
		}

		public Type ModelType
		{
			get;
			private set;
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
		}

		public override void OnLoadComplete(EventArgs e)
		{
			base.OnLoadComplete(e);

			AddControllers();

			LoadControllers(new ActionControllerManager(this).ValidControllers().ToList());

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

		protected virtual void AddControllers()
		{
			Controllers.Add(new ActionActiveWindowsController(this, ModelType, CurrentObject));
			Controllers.Add(new ActionCloseController(this, ModelType, CurrentObject));
			Controllers.Add(new ActionNewController(this, ModelType, CurrentObject));
			Controllers.Add(new ActionRefreshListController(this, ModelType, CurrentObject));
			Controllers.Add(new ActionOpenObjectListController(this, ModelType, CurrentObject));
			Controllers.Add(new ActionDeleteController(this, ModelType, CurrentObject));
			Controllers.Add(new ActionSaveController(this, ModelType, CurrentObject));
			Controllers.Add(new ActionRefreshDetailController(this, ModelType, CurrentObject));
			Controllers.Add(new ActionFileDataDetailController(this, ModelType, CurrentObject));
			Controllers.Add(new ActionFileDataListController(this, ModelType, CurrentObject));
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
	}
}
