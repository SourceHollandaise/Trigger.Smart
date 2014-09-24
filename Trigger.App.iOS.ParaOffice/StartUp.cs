using System;
using Trigger.BCL.ParaOffice;

namespace Trigger.App.ParaOffice
{
    public class StartUp
    {
        [STAThread]
        static void Main()
        {
            var gen = new Eto.iOS.Platform();

            var application = new XForms.Platform.XFormsApplication(gen);

            application.InitalizeApplication(new IOSBootstrapper());
            application.Run();
        }
    }

    public class IOSBootstrapper : Bootstrapper
    {
        public override void RegisterViewDescriptors()
        {
            base.RegisterViewDescriptors();

            //Map.RegisterType<IMainViewTemplate, ReducedMainViewTemplate>();
        }
    }
}
