
using System;
using Trigger.App.EventTracker;

namespace Trigger.App.OSX.EventTracker
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
