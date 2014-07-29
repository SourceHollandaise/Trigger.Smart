using System.Collections.Generic;
using System;

namespace Trigger.XForms
{
    public abstract class ViewDescriptor
    {
        public const string EmptySpaceFieldName = "EmptySpace";

        public IList<GroupItemDescription> GroupItemDescriptions { get; set; }

        public IList<TabItemDescription> TabItemDescriptions { get; set; }
    }
}

