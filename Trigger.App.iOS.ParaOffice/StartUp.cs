using System;

namespace Trigger.App.ParaOffice
{
    public class StartUp
    {
        [STAThread]
        static void Main()
        {
            var p = new Eto.iOS.Platform();

            var application = new AppParaOffice(p);

            application.InitalizeApplication();
            application.Run();
        }
    }
}
