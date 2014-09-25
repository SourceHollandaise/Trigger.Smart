using System.Collections.Generic;
using XForms.Design;

namespace Trigger.BCL.ParaOffice
{

    public class RueckverkehrDokumentListDescriptor : ListViewDescriptor<ErvRueckverkehrDokument>
    {
        public RueckverkehrDokumentListDescriptor()
        {
            DefaultSorting = ColumnSorting.Ascending;
            DefaultSortProperty = Fields.GetName(m => m.Subject);

            FilePreviewMode = XForms.Store.FileDataMode.FilePreview;

            ColumnDescriptions = new List<ColumnDescription>
            {
                new ColumnDescription(Fields.GetName(m => m.Subject), 1){ ColumnHeaderText = "Dokument" },
                //new ColumnDescription(Fields.GetName(m => m.FileName), 2){ ColumnHeaderText = "Datei" },
            };
        }
    }

}
