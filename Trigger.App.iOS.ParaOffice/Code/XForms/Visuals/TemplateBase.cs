using Eto.Forms;
using System;
using Trigger.XForms.Controllers;
using System.Linq;
using Trigger.XStorable.DataStore;
using Trigger.XStorable.Dependency;

namespace Trigger.XForms.Visuals
{
    public abstract class TemplateBase : Form
    {
        public IStorable CurrentObject
        {
            get;
            private set;
        }

        public Type ModelType
        {
            get;
            private set;
        }

        protected IViewTemplateConfiguration CompactViewConfig
        {
            get
            {
                return DependencyMapProvider.Instance.ResolveType<IViewTemplateConfiguration>();
            }
        }

        protected TemplateBase(Type type, IStorable currentObject)
        {
            this.ModelType = type;
            this.CurrentObject = currentObject;
            this.ID = this.Title;
		
            if (this.ToolBar == null)
                this.ToolBar = new ToolBar();

            //this.ToolBar.TextAlign = ToolBarTextAlign.Right;

//            if (this.Menu == null)
//                this.Menu = new MenuBar();
//
//            Application.Instance.CreateStandardMenu(Menu.Items);
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);

            LoadControllers();

//            if (this is DetailViewTemplate)
//                WindowManager.AddDetailView(CurrentObject, this as DetailViewTemplate);
//
//            if (this is ListViewTemplate)
//                WindowManager.AddListView(ModelType, this as ListViewTemplate);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (e.Modifiers == Keys.Control & e.Key == Keys.W)
                this.Close();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

//            if (this is ListViewTemplate)
//                WindowManager.RemoveListView(ModelType);
//
//            if (this is DetailViewTemplate)
//                WindowManager.RemoveDetailView(CurrentObject);
        }

        public void LoadControllers()
        {
            foreach (var controller in new ActionControllerProvider(this).ValidControllers().ToList())
            {
                if (controller.Visiblity == ActionVisibility.Menu)
                    continue;
                this.ToolBar.Items.AddRange(controller.Commands());
                /*
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
                */
            }
        }
    }
}
