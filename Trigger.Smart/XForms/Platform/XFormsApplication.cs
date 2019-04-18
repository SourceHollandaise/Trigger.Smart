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

        protected IDependencyMap Map => MapProvider.Instance;

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

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            if (LogonParams != null)
            {
                
                if (AutoLogon())
                {
                    ShowMainViewTemplate();
                }
                else
                    this.Quit();
            }
            else
            {
                new LogonViewTemplate().ShowModal(MainForm);
                ShowMainViewTemplate();
            }
        }

        protected virtual void ShowMainViewTemplate()
        {
           
            MainForm = Map.ResolveType<IMainViewTemplate>() as TemplateBase;
            MainForm.Show();
        }

        protected virtual bool AutoLogon()
        {
            var authenticate = Map.ResolveType<IAuthenticate>();

            var result = authenticate.LogOn(LogonParams);

            return result;
        }
    }
}
