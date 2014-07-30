using System;
using Trigger.XForms;
using System.Collections.Generic;

namespace Trigger.BCL.ParaOffice
{

    public class AktPersonListDescriptor : ListViewDescriptor<AktPerson>
    {
        public AktPersonListDescriptor()
        {
            ColumnDescriptions = new List<ColumnDescription>
            {
                new ColumnDescription(Fields.GetName(m => m.PersonAlias), 1){ ColumnHeaderText = "Person" },
                new ColumnDescription(Fields.GetName(m => m.AktAlias), 2){ ColumnHeaderText = "Akt" },
            };
        }
    }
}
