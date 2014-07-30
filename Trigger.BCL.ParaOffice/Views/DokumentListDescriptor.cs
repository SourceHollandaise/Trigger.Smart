using System;
using Trigger.XForms;
using System.Collections.Generic;

namespace Trigger.BCL.ParaOffice
{
    public class DokumentListDescriptor : ListDescriptor<Dokument>
    {
        public DokumentListDescriptor()
        {
            ColumnDescriptions = new List<ColumnDescription>
            {
                new ColumnDescription(Fields.GetName(m => m.Subject), 1){ ColumnHeaderText = "Dokument" },
                new ColumnDescription(Fields.GetName(m => m.RA), 2){ ColumnHeaderText = "RA" },
                new ColumnDescription(Fields.GetName(m => m.SK), 3){ ColumnHeaderText = "SK" },
                new ColumnDescription(Fields.GetName(m => m.Status), 4){ ColumnHeaderText = "Status" },
                new ColumnDescription(Fields.GetName(m => m.Medium), 5){ ColumnHeaderText = "Medium" },
                new ColumnDescription(Fields.GetName(m => m.Art), 6){ ColumnHeaderText = "Art" }
            };
        }
    }
}
