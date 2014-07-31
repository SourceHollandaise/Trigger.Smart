using Eto.Forms;
using System.Collections.Generic;
using Eto.Drawing;
using System.Linq;
using Trigger.XStorable.DataStore;
using Trigger.XForms.Controllers;

namespace Trigger.XForms.Visuals
{

    public class MainViewTemplate : TemplateBase
    {
        public MainViewTemplate() : base(typeof(IStorable), null)
        {
            Size = ViewTemplateConfig.ListViewCompactSize;

            Content = new MainViewGenerator(ModelTypesDeclarator.DeclaredModelTypes).GetContent();
            Content = new Scrollable{ Content = this.Content };
        }

        public override void OnKeyDown(KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control & e.Key == Keys.W)
            {
                var result = MessageBox.Show("Close application?", MessageBoxButtons.YesNo, MessageBoxType.Question, MessageBoxDefaultButton.No);
                if (result == DialogResult.Yes)
                    Application.Instance.Quit();
            }
            else
            {
                e.Handled = true;
            }
        }

        public override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            base.OnClosing(e);

            var templatesToClose = WindowManager.ActiveViews.ToList();
            if (templatesToClose != null)
                while (templatesToClose.Count > 0)
                {
                    templatesToClose[0].Close();
                    templatesToClose[0].Dispose();
                }
        }
    }
}
