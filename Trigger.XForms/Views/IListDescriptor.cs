using System.Collections.Generic;
using System;

namespace Trigger.XForms
{

    public interface IListDescriptor
    {
        IList<ColumnDescription> ColumnDescriptions { get; set; }
    }
}
