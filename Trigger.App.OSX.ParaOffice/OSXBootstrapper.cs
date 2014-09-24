using Trigger.BCL.ParaOffice;
using XForms.Design;

namespace Trigger.App.OSX.ParaOffice
{
    public class OSXBootstrapper : Bootstrapper
    {
        public override void RegisterViewDescriptors()
        {
            base.RegisterViewDescriptors();

            Map.RegisterType<IMainViewTemplate, ReducedMainViewTemplate>();
        }
    }
}
