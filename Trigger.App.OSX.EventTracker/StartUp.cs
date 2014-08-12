using System;
using Trigger.BCL.EventTracker;

namespace Trigger.App.OSX.EventTracker
{
    public class StartUp
    {
        [STAThread]
        static void Main()
        {
            var application = new XForms.Platform.XFormsApplication();

            application.InitalizeApplication(new Bootstrapper());
            application.Run();
        }
    }
}
