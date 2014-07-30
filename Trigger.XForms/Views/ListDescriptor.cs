using System.Collections.Generic;
using System;

namespace Trigger.XForms
{
    public abstract class ListDescriptor<T>
    {
        protected FieldNames<T> Fields = new FieldNames<T>();

        public IList<ColumnDescription> ColumnDescriptions { get; set; }
    }
}
