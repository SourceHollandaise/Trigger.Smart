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

			LoadControllers();

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

		public void LoadControllers()
		{
			foreach (var controller in new ActionControllerProvider(this).ValidControllers().ToList())
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
