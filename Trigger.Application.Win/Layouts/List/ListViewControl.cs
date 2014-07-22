using System;
using System.Linq;
using Eto.Forms;
using Trigger.Datastore.Persistent;
using Trigger.WinForms.Actions;
using Eto.Drawing;

namespace Trigger.WinForms.Layout
{
	//INFO: UserControl for GridView
	public class ListViewControl : Window
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

		public GridView CurrentGrid
		{
			get;
			protected set;
		}


		public ListViewControl(Type type, IPersistent currentObject) : base(Application.Instance.Generator, Application.Instance.Generator.GetType())
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

			if (CurrentGrid == null)
				CurrentGrid = new ListViewGenerator(ModelType).GetContent();

			Content = CurrentGrid;

			Size = new Size(-1, 360);
			Title = ModelType.Name + "-List - Items: " + CurrentGrid.DataStore.AsEnumerable().Count();
			
		}

		public override void OnLoadComplete(EventArgs e)
		{
			base.OnLoadComplete(e);

			LoadControllers();
		}

		public void LoadControllers()
		{
			foreach (var controller in new ActionControllerProvider().CreateControllers().ToList())
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
