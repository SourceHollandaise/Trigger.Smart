using System;

namespace Trigger.App.iOS.ParaOffice
{
    public class StartUp
    {
        [STAThread]
        static void Main()
        {
            var platForm = new Eto.iOS.Platform();

            var application = new AppParaOffice(platForm);

            application.InitalizeApplication();
            application.Run();
        }
    }
}
