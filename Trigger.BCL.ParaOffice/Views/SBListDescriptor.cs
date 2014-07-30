using System;
using Trigger.XForms;
using System.Collections.Generic;

namespace Trigger.BCL.ParaOffice
{

    public class SBListDescriptor : ListViewDescriptor<SB>
    {
        public SBListDescriptor()
        {
            ColumnDescriptions = new List<ColumnDescription>
            {
                new ColumnDescription(Fields.GetName(m => m.ID), 1){ ColumnHeaderText = "SB-Kürzel" },
                new ColumnDescription(Fields.GetName(m => m.User), 2){ ColumnHeaderText = "Benutzer" },
            };
        }
    }

}
