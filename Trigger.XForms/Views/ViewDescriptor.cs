using System.Collections.Generic;

namespace Trigger.XForms
{
    public abstract class ViewDescriptor
    {
        public IList<GroupItemDescription> GroupItemDescriptions { get; set; }

        public IList<TabItemDescription> TabItemDescriptions { get; set; }
    }
}

