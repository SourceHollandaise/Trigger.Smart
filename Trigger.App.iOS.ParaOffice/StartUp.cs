using System;

namespace Trigger.App.iOS.ParaOffice
{
    public class StartUp
    {
        [STAThread]
        static void Main()
        {
            var platform = new Eto.iOS.Platform();

            var application = new AppParaOffice(platform);

            application.InitalizeApplication();
            application.Run();
        }
    }
}
