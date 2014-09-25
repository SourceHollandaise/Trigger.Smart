using Trigger.BCL.EventTracker;
using XForms.Design;

namespace Trigger.App.OSX.EventTracker
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
