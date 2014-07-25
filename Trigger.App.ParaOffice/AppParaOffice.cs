//using GLib;
using System;
using Trigger.XForms.Visuals;
using Eto.Forms;
using Trigger.XStorable.Dependency;
using Trigger.BCL.Common.Security;

namespace Trigger.App.ParaOffice
{
    public class AppParaOffice : Eto.Forms.Application
    {
        public  void InitalizeApplication()
        {
            var init = new Bootstrapper();
            init.InitalizeDataStore();
            init.RegisterDependencies();
            init.CreateInitialObjects();
            init.RegisterDeclaredTypes();
        }

        public override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            var logonForm = new LogonViewTemplate();
            logonForm.Show();

            logonForm.Closed += (o, args) =>
            {


                MainForm = new MainViewTemplate();
                MainForm.Title = "User: " + DependencyMapProvider.Instance.ResolveInstance<ISecurityInfoProvider>().CurrentUser.UserName;
                MainForm.BringToFront();
                MainForm.Show();
            };
        }
    }
}
