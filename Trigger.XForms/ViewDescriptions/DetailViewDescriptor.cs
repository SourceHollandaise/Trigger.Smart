using System.Collections.Generic;
using System;

namespace Trigger.XForms
{
    public abstract class DetailViewDescriptor<T> : IDetailViewDescriptor
    {
        public IList<TabItemDescription> TabItemDescriptions { get; set; }

        public IList<GroupItemDescription> GroupItemDescriptions { get; set; }

        protected FieldNames<T> Fields = new FieldNames<T>();

        protected const string EmptySpaceFieldName = "EmptySpace";
    }
}

