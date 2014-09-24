using System.Collections.Generic;
using XForms.Commands;
using XForms.Design;

namespace Trigger.BCL.ParaOffice
{
    public class DokumentListDescriptor : ListViewDescriptor<Dokument>
    {
        public DokumentListDescriptor()
        {
            RegisterCommands<IUpdateDocumentStoreListViewCommand>();
            RegisterCommands<ISlideShowListViewCommand>();

            DefaultSorting = ColumnSorting.Ascending;
            DefaultSortProperty = Fields.GetName(m => m.Subject);

            ListDetailView = true;
            ListDetailViewColumns = 2;
            ListDetailViewWithToolbar = true;
            ListDetailViewOrientation = ViewItemOrientation.Horizontal;

            DetailView = new DokumentListDetailViewDescriptor();

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
