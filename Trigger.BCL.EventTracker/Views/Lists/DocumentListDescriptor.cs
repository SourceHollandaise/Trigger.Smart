using XForms.Design;
using System.Collections.Generic;
using Trigger.BCL.EventTracker.Model;
using XForms.Commands;

namespace Trigger.BCL.EventTracker
{
    public class DocumentListDescriptor : ListViewDescriptor<Document>
    {
        public DocumentListDescriptor()
        {
            RegisterCommands<IUpdateDocumentStoreListViewCommand>();
            RegisterCommands<IFileViewerListViewCommand>();
  
            DefaultSorting = ColumnSorting.Ascending;
            DefaultSortProperty = Fields.GetName(m => m.Subject);

//            ListDetailView = true;
//            ListDetailViewWithToolbar = true;
//            ListDetailViewColumns = 2;
//            ListDetailViewOrientation = ViewItemOrientation.Vertical;
//
//            DetailView = new DocumentListDetailViewDescriptor();

            FilePreviewMode = XForms.Store.FileDataMode.MixedMode;

            ColumnDescriptions = new List<ColumnDescription>
            {
                new ColumnDescription(Fields.GetName(m => m.Subject), 1){ ColumnHeaderText = "Subject" },
                new ColumnDescription(Fields.GetName(m => m.AreaAlias), 2){ ColumnHeaderText = "Area" },
                new ColumnDescription(Fields.GetName(m => m.IssueAlias), 3){ ColumnHeaderText = "Issue" },
            };
        }
    }
}
