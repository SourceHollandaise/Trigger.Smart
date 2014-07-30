using System.Collections.Generic;
using System;

namespace Trigger.XForms
{

    public interface IViewDescriptor
    {
        IList<GroupItemDescription> GroupItemDescriptions { get; set; }

        IList<TabItemDescription> TabItemDescriptions { get; set; }
    }
}
