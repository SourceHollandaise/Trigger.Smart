using System;
using Eto.Forms;
using System.Collections.Generic;
using Eto.Drawing;
using System.Linq;
using Trigger.XStorable.DataStore;
using Trigger.XForms.Controllers;
using Trigger.XStorable.Dependency;
using Trigger.BCL.Common.Security;

namespace Trigger.XForms.Visuals
{
    public class MainViewTemplate : TemplateBase
    {
        public MainViewTemplate() : base(typeof(IStorable), null)
        {
            Size = new Size(400, 768);

            Content = new MainViewGenerator(ModelTypesDeclaration.DeclaredModelTypes).GetContent();
            Content = new Scrollable{ Content = this.Content };
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);

//            var logon = new LogonViewTemplate();
// 
//            logon.Show();
//
//            logon.Closed += (sender, args) =>
//            {
//                Title = "User: " + DependencyMapProvider.Instance.ResolveInstance<ISecurityInfoProvider>().CurrentUser.UserName;
//            };
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control & e.Key == Keys.W)
            {
                var result = MessageBox.Show("Close application?", MessageBoxButtons.YesNo, MessageBoxType.Question, MessageBoxDefaultButton.No);
                if (result == DialogResult.Yes)
                    Application.Instance.Quit();
            }
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            base.OnClosing(e);

            var templatesToClose = WindowManager.ActiveViews.ToList();
            while (templatesToClose.Count > 0)
            {
                templatesToClose[0].Close();
                templatesToClose[0].Dispose();
            }
        }
    }
}