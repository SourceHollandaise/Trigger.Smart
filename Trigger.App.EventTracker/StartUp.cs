using System;
using Trigger.XForms.Visuals;

namespace Trigger.App.EventTracker
{
    public class StartUp
    {
        [STAThread]
        static void Main()
        {
            var application = new AppEventTracker();

            application.InitalizeApplication();
            application.Run();
        }
    }
}
