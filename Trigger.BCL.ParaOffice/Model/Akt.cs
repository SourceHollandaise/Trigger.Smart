using System;
using Trigger.XStorable.Model;
using System.Collections.Generic;
using System.Linq;
using Trigger.XStorable.DataStore;

namespace Trigger.BCL.ParaOffice
{

    [System.ComponentModel.DefaultProperty("Bezeichnung")]
    public class Akt : StorableBase
    {
        public string Bezeichnung
        {
            get;
            set;
        }

        public string Bemerkung
        {
            get;

            set;
        }

        public AktArt AktArt
        {
            get;
            set;
        }

        public DateTime AnlageDatum
        {
            get;
            set;
        }

        public DateTime? ErledigungDatum
        {
            get;
            set;
        }

        public SB SB1
        {
            get;
            set;
        }

        public SB SB2
        {
            get;
            set;
        }

        public SB SB3
        {
            get;
            set;
        }

        [System.ComponentModel.DisplayName("Dokumente zu Akt")]
        [System.Runtime.Serialization.IgnoreDataMember]
        [LinkedList(typeof(Dokument))]
        public IEnumerable<Dokument> LinkedDokumente
        {
            get
            {
                return Store.LoadAll<Dokument>().Where(p => p.Akt != null && p.MappingId == this.MappingId);
            }
        }
    }
}

