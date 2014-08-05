using System;
using System.Collections.Generic;
using System.Linq;
using Trigger.XForms;
using Trigger.XStorable.DataStore;
using Trigger.XStorable.Dependency;

namespace Trigger.BCL.ParaOffice
{

    public class TelefonatListDescriptor : ListViewDescriptor<Telefonat>
    {
        public TelefonatListDescriptor()
        {
            DefaultSorting = ColumnSorting.Descendig;
            DefaultSortProperty = Fields.GetName(m => m.Beginn);

            ColumnDescriptions = new List<ColumnDescription>
            {
                new ColumnDescription(Fields.GetName(m => m.Beginn), 1){ ColumnHeaderText = "Datum/Uhrzeit" },
                new ColumnDescription(Fields.GetName(m => m.Teilnehmer), 2){ ColumnHeaderText = "Gesprächspartner" },
                new ColumnDescription(Fields.GetName(m => m.AktAlias), 3){ ColumnHeaderText = "Akt" },
                new ColumnDescription(Fields.GetName(m => m.SB1Alias), 4){ ColumnHeaderText = "Telefonist" },
                new ColumnDescription(Fields.GetName(m => m.SB2Alias), 52){ ColumnHeaderText = "Für SB" },
                new ColumnDescription(Fields.GetName(m => m.Art), 6){ ColumnHeaderText = "Art" },
                new ColumnDescription(Fields.GetName(m => m.Status), 7){ ColumnHeaderText = "Status" },
            };
        }

        public override IEnumerable<IStorable> Repository
        {
            get
            {
                var store = DependencyMapProvider.Instance.ResolveType<IStore>();

                return store.LoadAll<SB>().Where(p => p.TelefonatAnzeigen).SelectMany(p => p.LinkedTelefonate) as IEnumerable<IStorable>;
            }
        }
    }
}
