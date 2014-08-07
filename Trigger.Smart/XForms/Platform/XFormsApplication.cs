using Eto.Forms;
using System;
using XForms.Design;

namespace XForms.Platform
{
    public class XFormsApplication : Application
    {
        public virtual void InitalizeApplication(BootstrapperBase bootstrapper)
        {
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

            var logonForm = new LogonViewTemplate();
            if (logonForm.ShowDialog() == DialogResult.Ok)
            {
                MainForm = new MainViewTemplate();
                MainForm.Show();
            }
        }
    }
}
