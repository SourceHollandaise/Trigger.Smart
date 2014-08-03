
using System;
using Trigger.XForms.Visuals;
using Eto.Forms;

namespace Trigger.App.EventTracker
{
    public class AppEventTracker : Application
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
                MainForm.Show();
            }
        }
    }
}
