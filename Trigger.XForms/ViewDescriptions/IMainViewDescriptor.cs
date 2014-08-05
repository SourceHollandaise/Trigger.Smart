using System.Collections.Generic;

namespace Trigger.XForms
{

    public interface IMainViewDescriptor
    {
        IList<NavigationGroupItem> NavigationGroups { get; set; }
    }
    
}
