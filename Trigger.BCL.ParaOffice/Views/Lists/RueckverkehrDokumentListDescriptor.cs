using System.Collections.Generic;
using XForms.Design;

namespace Trigger.BCL.ParaOffice
{

    public class RueckverkehrDokumentListDescriptor : ListViewDescriptor<RueckverkehrDokument>
    {
        public RueckverkehrDokumentListDescriptor()
        {
            DefaultSorting = ColumnSorting.Ascending;
            DefaultSortProperty = Fields.GetName(m => m.Subject);

            ColumnDescriptions = new List<ColumnDescription>
            {
                new ColumnDescription(Fields.GetName(m => m.Subject), 1){ ColumnHeaderText = "Dokument" },
                new ColumnDescription(Fields.GetName(m => m.FileName), 2){ ColumnHeaderText = "Datei" },
            };
        }
    }

}
