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
            internal set;
        }

        public Type ModelType
        {
            get;
            internal set;
        }

        protected IViewTemplateConfiguration ViewTemplateConfig
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
            this.BackgroundColor = Eto.Drawing.Colors.White;
		
            if (this.ToolBar == null)
                this.ToolBar = new ToolBar();

            this.ToolBar.TextAlign = ToolBarTextAlign.Right;
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);

            LoadControllers();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (e.Modifiers == Keys.Control & e.Key == Keys.W)
                this.Close();
        }

        public void LoadControllers()
        {
            foreach (var controller in new ActionControllerProvider(this).ValidControllers().ToList())
            {
                if (controller.Visiblity != ActionVisibility.Menu)
                    this.ToolBar.Items.AddRange(controller.Commands());
            }
        }
    }
}
