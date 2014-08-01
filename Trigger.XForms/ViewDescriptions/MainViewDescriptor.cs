using System.Collections.Generic;
using System;
using Trigger.XStorable.Dependency;
using Trigger.XForms.Commands;

namespace Trigger.XForms
{

    public abstract class MainViewDescriptor : IMainViewDescriptor
    {
        public IList<NavigationGroupItem> NavigationGroups { get; set; }
    }
    
}
