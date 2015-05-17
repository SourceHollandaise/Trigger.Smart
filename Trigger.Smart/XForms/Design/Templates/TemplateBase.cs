using Eto.Forms;
using System;
using XForms.Store;
using Eto.Drawing;

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

        protected TemplateBase(Type type, IStorable currentObject)
        {
            this.ModelType = type;
            this.CurrentObject = currentObject;

            this.BackgroundColor = Colors.White;
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (e.Modifiers == Keys.Control & e.Key == Keys.W)
                this.Close();

            if (e.Modifiers == Keys.Control & e.Key == Keys.Space)
                new SearchFormTemplate().Show();
        }
    }
}
