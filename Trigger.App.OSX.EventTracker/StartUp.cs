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

        /*
        static void AddStyles()
        {
            // support full screen mode!
            Style.Add<FormHandler>("main", handler =>
            {
                handler.Control.CollectionBehavior |= NSWindowCollectionBehavior.FullScreenPrimary;
            });

            Style.Add<ApplicationHandler>("application", handler =>
            {
                handler.EnableFullScreen();
            });
        }
        */
    }
}
