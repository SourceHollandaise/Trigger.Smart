using Eto.Forms;
using System;
using Eto.Drawing;
using XForms.Store;
using XForms.Dependency;

namespace XForms.Design
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
                return MapProvider.Instance.ResolveType<IViewTemplateConfiguration>();
            }
        }

        protected TemplateBase(Type type, IStorable currentObject)
        {
            this.ModelType = type;
            this.CurrentObject = currentObject;
            this.BackgroundColor = Colors.WhiteSmoke;
        }

        public override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (e.Modifiers == Keys.Control & e.Key == Keys.W)
                this.Close();
        }
    }
}
