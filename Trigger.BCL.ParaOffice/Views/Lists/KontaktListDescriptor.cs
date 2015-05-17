using System.Collections.Generic;
using XForms.Design;

namespace Trigger.BCL.ParaOffice
{

    public class KontaktListDescriptor : ListViewDescriptor<Kontakt>
    {
        public KontaktListDescriptor()
        {
            DefaultSorting = ColumnSorting.Ascending;
            DefaultSortProperty = Fields.GetName(m => m.PersonAlias);

//            ListDetailView = true;
//            ListDetailViewWithToolbar = true;
//            ListDetailViewColumns = 3;
//            ListDetailViewOrientation = ViewItemOrientation.Vertical;
//
//            DetailView = new KontaktListDetailViewDescriptor();

            ColumnDescriptions = new List<ColumnDescription>
            {
                new ColumnDescription(Fields.GetName(m => m.PersonAlias), 1){ ColumnHeaderText = "Person" },
                new ColumnDescription(Fields.GetName(m => m.Organisation), 2){ ColumnHeaderText = "Unternehmen" },
                new ColumnDescription(Fields.GetName(m => m.MobilTelefon), 3){ ColumnHeaderText = "Mobil" },
                new ColumnDescription(Fields.GetName(m => m.Telefon), 4){ ColumnHeaderText = "Telefon" },
                new ColumnDescription(Fields.GetName(m => m.Email), 5){ ColumnHeaderText = "E-Mail" },
                new ColumnDescription(Fields.GetName(m => m.WebSite), 6){ ColumnHeaderText = "Web" },
            };
        }
    }
}
