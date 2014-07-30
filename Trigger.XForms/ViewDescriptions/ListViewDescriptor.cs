using System.Collections.Generic;
using System;

namespace Trigger.XForms
{
    public abstract class ListViewDescriptor<T> : IListViewDescriptor
    {
        public IList<ColumnDescription> ColumnDescriptions { get; set; }

        public bool AllowColumnReorder { get; set; }

        public bool AllowMultiSelection { get; set; }

        protected FieldNames<T> Fields = new FieldNames<T>();

        protected ListViewDescriptor()
        {
            AllowColumnReorder = true;
            AllowMultiSelection = false;
        }
    }
}
