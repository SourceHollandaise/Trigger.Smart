using System;
using Trigger.BCL.ParaOffice;
using XForms.Design;
using XForms.Security;
using System.Diagnostics;

namespace Trigger.App.OSX.ParaOffice
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

            application.InitalizeApplication(new OSXBootstrapper(), logon);

            application.Run();
        }
    }
}
