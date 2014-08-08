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
            ListDetailView = true;
            ListDetailViewWithToolbar = false;
            ListDetailViewColumns = 2;
            ListDetailViewOrientation = ViewItemOrientation.Vertical;


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
