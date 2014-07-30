using System;
using Trigger.XForms;
using System.Collections.Generic;

namespace Trigger.BCL.ParaOffice
{

    public class AktListDescriptor : ListViewDescriptor<Akt>
    {
        public AktListDescriptor()
        {
            ColumnDescriptions = new List<ColumnDescription>
            {
                new ColumnDescription(Fields.GetName(m => m.Bezeichnung), 1){ ColumnHeaderText = "Bezeichnung" },
                new ColumnDescription(Fields.GetName(m => m.AktArtAlias), 2){ ColumnHeaderText = "Aktart" },
            };
        }
    }

}
