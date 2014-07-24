using GLib;
using System;

namespace Trigger.App.ParaOffice
{
    public class StartUp
    {
        [STAThread]
        static void Main()
        {
            var application = new AppParaOffice();

            application.InitalizeApplication();
            application.Run();

            ExceptionManager.UnhandledException += (args) =>
            {
                Log.DefaultHandler("App", LogLevelFlags.FlagFatal & LogLevelFlags.Critical, args.ExceptionObject.ToString());
            };

        }
    }
}
