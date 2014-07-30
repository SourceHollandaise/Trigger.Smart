using System.Collections.Generic;
using System;

namespace Trigger.XForms
{
    public abstract class ListViewDescriptor<T> : IListViewDescriptor
    {
        protected ListViewDescriptor()
        {
            AllowColumnReorder = true;
            AllowMultiSelection = false;
        }

        protected FieldNames<T> Fields = new FieldNames<T>();

        public bool AllowColumnReorder { get; set; }

        public bool AllowMultiSelection { get; set; }

        public IList<ColumnDescription> ColumnDescriptions { get; set; }
    }
}
