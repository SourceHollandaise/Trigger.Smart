using System.Collections.Generic;
using XForms.Design;

namespace Trigger.BCL.ParaOffice
{
    public class RueckverkehrListDescriptor : ListViewDescriptor<Rueckverkehr>
    {
        public RueckverkehrListDescriptor()
        {
            RegisterCommands<IRueckverkehrAbholenListViewCommand>();

            DefaultSorting = ColumnSorting.Ascending;
            DefaultSortProperty = Fields.GetName(m => m.HinterlegungDatum);

            ColumnDescriptions = new List<ColumnDescription>
            {
                new ColumnDescription(Fields.GetName(m => m.ErvCode), 1){ ColumnHeaderText = "ERV-Code" },
                new ColumnDescription(Fields.GetName(m => m.Art), 2){ ColumnHeaderText = "Art" },
                new ColumnDescription(Fields.GetName(m => m.AnzahlDokumentAnhang), 3){ ColumnHeaderText = "Anhänge" },
                new ColumnDescription(Fields.GetName(m => m.EmpfangDatum), 4){ ColumnHeaderText = "Empfangen" },
                new ColumnDescription(Fields.GetName(m => m.HinterlegungDatum), 4){ ColumnHeaderText = "Hinterlegt" },
            };
        }
    }
}
