using System.Collections.Generic;

namespace XForms.Design
{

    public abstract class MainViewDescriptor : IMainViewDescriptor
    {
        public IList<NavigationGroupItem> NavigationGroups { get; set; }
    }
}
