using System.Collections.Generic;
using System;

namespace Trigger.XForms
{
    public abstract class ViewDescriptor<T> : IViewDescriptor
    {
        public IList<GroupItemDescription> GroupItemDescriptions { get; set; }

        public IList<TabItemDescription> TabItemDescriptions { get; set; }

        protected FieldNames<T> Fields = new FieldNames<T>();

        protected const string EmptySpaceFieldName = "EmptySpace";
    }
}

