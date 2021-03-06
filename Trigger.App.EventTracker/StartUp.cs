using System;
using Trigger.BCL.EventTracker;
using XForms.Design;
using XForms.Security;

namespace Trigger.App.EventTracker
{
    public class StartUp
    {
        [STAThread]
        static void Main()
        {
            var application = new XForms.Platform.XFormsApplication();

            LogonParameters logon = null;

            if (System.Diagnostics.Debugger.IsAttached)
            {
                logon = new LogonParameters
                {
                    UserName = "Admin",
                    Password = "admin"
                };
            }

            application.InitalizeApplication(new AppBootstrapper(), logon);
            application.Run();
        }
    }
}
