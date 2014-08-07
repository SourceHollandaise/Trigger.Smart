using System.Collections.Generic;

namespace XForms.Design
{
    public interface IMainViewDescriptor
    {
        IList<NavigationGroupItem> NavigationGroups { get; set; }
    }
}
