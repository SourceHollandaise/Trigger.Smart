//using GLib;
using System;
using Trigger.XForms.Visuals;
using Eto.Forms;
using Trigger.XStorable.Dependency;
using Trigger.BCL.Common.Security;

namespace Trigger.App.iOS.ParaOffice
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

            MainForm = new MainViewTemplate();

            MainForm.Show();
        }
    }
}
