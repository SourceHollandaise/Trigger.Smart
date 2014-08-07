using System;
using Trigger.XForms.Visuals;

namespace Trigger.App.EventTracker
{
    public class StartUp
    {
        [STAThread]
        static void Main()
        {
            var gen = new Eto.iOS.Platform();

            var application = new AppEventTracker(gen);

            application.InitalizeApplication();
            application.Run();
        }
    }
}
