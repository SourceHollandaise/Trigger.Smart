using System.Collections.Generic;
using System;

namespace Trigger.XForms
{
    public interface IListViewDescriptor
    {
        bool AllowColumnReorder { get; set; }

        bool AllowMultiSelection { get; set; }

        IList<ColumnDescription> ColumnDescriptions { get; set; }
    }
}
