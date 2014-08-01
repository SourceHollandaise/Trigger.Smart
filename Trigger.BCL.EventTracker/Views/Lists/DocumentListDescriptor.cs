using Trigger.XForms;
using System.Collections.Generic;
using Trigger.BCL.EventTracker.Model;
using Trigger.XForms.Commands;

namespace Trigger.BCL.EventTracker
{
    public class DocumentListDescriptor : ListViewDescriptor<Document>
    {
        public DocumentListDescriptor()
        {
            RegisterCommands<IUpdateDocumentStoreCommand>();

            ColumnDescriptions = new List<ColumnDescription>
            {
                new ColumnDescription(Fields.GetName(m => m.Subject), 1){ ColumnHeaderText = "Subject" },
                new ColumnDescription(Fields.GetName(m => m.FileName), 2){ ColumnHeaderText = "File" },
            };
        }
    }
}
