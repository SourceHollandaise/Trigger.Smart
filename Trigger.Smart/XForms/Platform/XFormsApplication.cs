using System;
using Eto.Forms;
using XForms.Design;
using XForms.Security;
using XForms.Dependency;

namespace XForms.Platform
{
    public class XFormsApplication : Application
    {
        protected LogonParameters LogonParams;

        protected IDependencyMap Map
        {
            get
            {
                return MapProvider.Instance;
            }
        }

        public virtual void InitalizeApplication(BootstrapperBase bootstrapper, LogonParameters logonParams = null)
        {
            LogonParams = logonParams;

            bootstrapper.InitalizeDataStore();
            bootstrapper.InitialiteSecurityProvider();
            bootstrapper.RegisterDependencies();
            bootstrapper.RegisterViewDescriptors();
            bootstrapper.RegisterCommands();
            bootstrapper.CreateDefaultUser();
        }

        public override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            if (LogonParams != null)
            {
                if (AutoLogon())
                {
                    MainForm = new MainViewTemplate();
                    MainForm.Show();
                }
                else
                    this.Quit();
            }
            else
            {
                var logonForm = new LogonViewTemplate();
                if (logonForm.ShowDialog() == DialogResult.Ok)
                {
                    MainForm = new MainViewTemplate();
                    MainForm.Show();
                }
            }
        }

        protected virtual bool AutoLogon()
        {
            var authenticate = Map.ResolveType<IAuthenticate>();

            var result = authenticate.LogOn(LogonParams);

            return result;
        }
    }
}
