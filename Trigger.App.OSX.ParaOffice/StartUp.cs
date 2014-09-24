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

            var logon = new LogonParameters()
            {
                UserName = "JE",
                Password = ""
            };

            application.InitalizeApplication(new OSXBootstrapper(), Debugger.IsAttached ? logon : null);
            application.Run();
        }
    }
}
