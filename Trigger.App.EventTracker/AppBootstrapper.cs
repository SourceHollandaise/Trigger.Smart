using System;
using Trigger.BCL.EventTracker;
using XForms.Design;
using XForms.Security;

namespace Trigger.App.EventTracker
{

    public class AppBootstrapper : Bootstrapper
    {
        public override void RegisterViewDescriptors()
        {
            base.RegisterViewDescriptors();

            //Map.RegisterType<IMainViewTemplate, MainViewTemplate>();
        }
    }
}
