using System;

namespace Trigger.App.EventTracker
{
    public class StartUp
    {
        [STAThread]
        static void Main()
        {
            var p = new Eto.iOS.Platform();

            var application = new AppEventTracker(p);

            application.InitalizeApplication();
            application.Run();
        }
    }
}
