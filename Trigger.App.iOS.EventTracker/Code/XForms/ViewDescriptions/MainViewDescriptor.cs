using System.Collections.Generic;

namespace Trigger.XForms
{

    public abstract class MainViewDescriptor : IMainViewDescriptor
    {
        public IList<NavigationGroupItem> NavigationGroups { get; set; }
    }
}
