using System;
using Trigger.XForms;
using System.Collections.Generic;
using Trigger.XForms.Commands;

namespace Trigger.BCL.ParaOffice
{
    public class DokumentListDescriptor : ListViewDescriptor<Dokument>
    {
        public DokumentListDescriptor()
        {
            RegisterCommands<IUpdateDocumentStoreListViewCommand>();

            ColumnDescriptions = new List<ColumnDescription>
            {
                new ColumnDescription(Fields.GetName(m => m.Subject), 1){ ColumnHeaderText = "Dokument" },
                new ColumnDescription(Fields.GetName(m => m.RAAlias), 2){ ColumnHeaderText = "RA" },
                new ColumnDescription(Fields.GetName(m => m.SKAlias), 3){ ColumnHeaderText = "SK" },
                new ColumnDescription(Fields.GetName(m => m.Status), 4){ ColumnHeaderText = "Status" },
                new ColumnDescription(Fields.GetName(m => m.Medium), 5){ ColumnHeaderText = "Medium" },
                new ColumnDescription(Fields.GetName(m => m.Art), 6){ ColumnHeaderText = "Art" },
                new ColumnDescription(Fields.GetName(m => m.AktAlias), 7){ ColumnHeaderText = "Akt" },
            };
        }
    }
}
