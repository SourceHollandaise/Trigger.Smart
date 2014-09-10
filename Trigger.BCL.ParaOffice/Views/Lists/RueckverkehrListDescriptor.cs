using System.Collections.Generic;
using XForms.Design;
using XForms.Store;
using XForms.Dependency;
using System.Linq;

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

        public override IEnumerable<IStorable> Repository
        {
            get
            {
                var store = MapProvider.Instance.ResolveType<IStore>();

                var currentSBErvCode = ApplicationModelQuery.CurrentSB.ErvCode;

                return store.LoadAll<Rueckverkehr>().Where(p => p.ErvCode.Equals(currentSBErvCode)) as IEnumerable<IStorable>;
            }
        }
    }
}
