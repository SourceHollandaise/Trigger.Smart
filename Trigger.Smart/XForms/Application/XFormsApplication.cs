using System.Linq;
using XForms.Dependency;
using XForms.Commands;
using XForms.Store;
using XForms.Model;

namespace XForms.Application
{

    public class XFormsApplication : Application
    {
        public virtual void InitalizeApplication(Bootstrapper bootstrapper)
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
