using System;

namespace Trigger.App.ParaOffice
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
