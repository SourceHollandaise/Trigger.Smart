using System;
using Trigger.XForms;
using System.Collections.Generic;

namespace Trigger.BCL.ParaOffice
{

    public class KontaktListDescriptor : ListViewDescriptor<Kontakt>
    {
        public KontaktListDescriptor()
        {
            ColumnDescriptions = new List<ColumnDescription>
            {
                new ColumnDescription(Fields.GetName(m => m.PersonAlias), 1){ ColumnHeaderText = "Person" },
                new ColumnDescription(Fields.GetName(m => m.Art), 2){ ColumnHeaderText = "Art" },
                new ColumnDescription(Fields.GetName(m => m.MobilTelefon), 3){ ColumnHeaderText = "Mobil" },
                new ColumnDescription(Fields.GetName(m => m.Telefon), 4){ ColumnHeaderText = "Telefon" },
                new ColumnDescription(Fields.GetName(m => m.Email), 5){ ColumnHeaderText = "E-Mail" },
            };
        }
    }
    
}
