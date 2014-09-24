using System;
using XForms.Security;
using System.Diagnostics;

namespace Trigger.App.Android.ParaOffice
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

            application.InitalizeApplication(new AndroidBootstrapper(), Debugger.IsAttached ? logon : null);
            application.Run();
        }
    
    }
