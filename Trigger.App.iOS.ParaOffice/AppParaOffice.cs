//using GLib;
using System;
using Trigger.XForms.Visuals;
using Eto.Forms;

namespace Trigger.App.ParaOffice
{
    public class AppParaOffice : Application
    {
        public AppParaOffice(Eto.Platform platform) : base(platform)
        {

        }

        public virtual void InitalizeApplication()
        {
            var init = new Bootstrapper();
            init.InitalizeDataStore();
            init.RegisterDependencies();
            init.CreateInitialObjects();
            init.RegisterDeclaredTypes();
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

//            var logonForm = new LogonViewTemplate();
//            logonForm.Show();
//
//            logonForm.Closed += (o, args) =>
//            {
            MainForm = new MainViewTemplate();
            MainForm.BringToFront();
            MainForm.Show();
            //};
        }
    }
}
