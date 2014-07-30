using Trigger.XForms;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;
using Trigger.BCL.EventTracker.Model;

namespace Trigger.BCL.EventTracker
{
    public class AreaListDescriptor : ListDescriptor
    {
        public AreaListDescriptor()
        {
            ColumnDescriptions = new List<ColumnDescription>
            {
                new ColumnDescription("Area.Name", 1){ ColumnHeaderText = "Area" },
                new ColumnDescription("Area.Description", 1){ ColumnHeaderText = "Area Description" },
            };
        }
    }
}
