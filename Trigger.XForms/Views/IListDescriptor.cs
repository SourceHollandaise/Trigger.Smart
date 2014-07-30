using System.Collections.Generic;
using System;

namespace Trigger.XForms
{
    public interface IListDescriptor
    {
        bool AllowColumnReorder { get; set; }

        bool AllowMultiSelection { get; set; }

        IList<ColumnDescription> ColumnDescriptions { get; set; }
    }
}
