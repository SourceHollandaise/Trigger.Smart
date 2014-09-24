using Trigger.BCL.ParaOffice;
using XForms.Design;

namespace Trigger.App.Android.ParaOffice
{
    public class AndroidBootstrapper : Bootstrapper
    {
        public override void RegisterViewDescriptors()
        {
            base.RegisterViewDescriptors();

            Map.RegisterType<IMainViewTemplate, ReducedMainViewTemplate>();
        }
    }
}
