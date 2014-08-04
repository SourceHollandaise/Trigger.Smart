using Trigger.XForms;
using System.Linq;
using System.Collections.Generic;
using Trigger.XStorable.DataStore;
using Trigger.XStorable.Dependency;
using Trigger.BCL.ParaOffice;

namespace Trigger.BCL.ParaOffice
{

    public class TerminListDescriptor : ListViewDescriptor<Termin>
    {
        public TerminListDescriptor()
        {
            ColumnDescriptions = new List<ColumnDescription>
            {
                new ColumnDescription(Fields.GetName(m => m.Art), 1){ ColumnHeaderText = "Art" },
                new ColumnDescription(Fields.GetName(m => m.Betreff), 2){ ColumnHeaderText = "Betreff" },
                new ColumnDescription(Fields.GetName(m => m.Beginn), 3){ ColumnHeaderText = "Beginn" },
                new ColumnDescription(Fields.GetName(m => m.AktAlias), 4){ ColumnHeaderText = "Akt" },
                new ColumnDescription(Fields.GetName(m => m.KlientGegner), 5){ ColumnHeaderText = "Klient/Gegner" },
                new ColumnDescription(Fields.GetName(m => m.SBAlias), 5){ ColumnHeaderText = "SB" },
                new ColumnDescription(Fields.GetName(m => m.OK), 5){ ColumnHeaderText = "Erledigt" },
            };
        }

        public override IEnumerable<IStorable> Repository
        {
            get
            {
                var store = DependencyMapProvider.Instance.ResolveType<IStore>();

                return store.LoadAll<SB>().Where(p => p.TermineAnzeigen).SelectMany(p => p.LinkedTermine) as IEnumerable<IStorable>;
            }
        }

        /*
        public override Func<IStorable, bool> Filter
        {
            get
            {
                return m =>
                {
                    var termin = m as Termin;
                    return termin != null && termin.SB != null;

                };
            }
            set
            {
                base.Filter = value;
            }
        }
        */
    }
}
