
using System;
using Trigger.XForms.Visuals;
using Eto.Forms;
using Trigger.BCL.Common.Security;
using Trigger.XStorable.Dependency;

namespace Trigger.App.EventTracker
{
    public class AppEventTracker : Eto.Forms.Application
    {
        public AppEventTracker(Eto.Platform p) : base(p)
        {

        }

        public  void InitalizeApplication()
        {
            var init = new Bootstrapper();

            init.InitalizeDataStore();
            init.RegisterDependencies();
            init.CreateInitialObjects();
            init.InitialiteSecurityProvider();
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            var auth = DependencyMapProvider.Instance.ResolveType<IAuthenticate>();

            auth.LogOn(new LogonParameters{ UserName = "Jörg", Password = null });

            MainForm = new MainViewTemplate();
            MainForm.Show();
        }
    }
}
