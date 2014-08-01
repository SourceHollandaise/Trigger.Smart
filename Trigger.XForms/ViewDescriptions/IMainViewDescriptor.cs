using System.Collections.Generic;
using System;
using Trigger.XStorable.Dependency;
using Trigger.XForms.Commands;

namespace Trigger.XForms
{

    public interface IMainViewDescriptor
    {
        IList<NavigationGroupItem> NavigationGroups { get; set; }
    }
    
}
