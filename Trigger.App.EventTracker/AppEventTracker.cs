//using GLib;
using System;
using Trigger.XForms.Visuals;
using Eto.Forms;
using Trigger.XStorable.Dependency;
using Trigger.BCL.Common.Security;

namespace Trigger.App.EventTracker
{
    public class AppEventTracker : Eto.Forms.Application
    {
        public virtual void InitalizeApplication()
        {
            var init = new Bootstrapper();
            init.InitialiteSecurityProvider();
            init.InitalizeDataStore();
            init.RegisterDependencies();
            init.CreateInitialObjects(); 
        }

        public override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            var logonForm = new LogonViewTemplate();
            if (logonForm.ShowDialog() == DialogResult.Ok)
            {
                MainForm = new MainViewTemplate();
                //MainForm.Title = "User: " + DependencyMapProvider.Instance.ResolveInstance<ISecurityInfoProvider>().CurrentUser.UserName;
                MainForm.Show();
            }
        }
    }
}
