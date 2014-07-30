using System;
using Trigger.XForms;
using System.Collections.Generic;

namespace Trigger.BCL.ParaOffice
{

    public class AktArtListDescriptor : ListDescriptor<AktArt>
    {
        public AktArtListDescriptor()
        {
            ColumnDescriptions = new List<ColumnDescription>
            {
                new ColumnDescription(Fields.GetName(m => m.Art), 1){ ColumnHeaderText = "Art" },
                new ColumnDescription(Fields.GetName(m => m.Bemerkung), 2){ ColumnHeaderText = "Bemerkung" },
            };
        }
    }

}
