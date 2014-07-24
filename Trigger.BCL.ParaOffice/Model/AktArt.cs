using System;
using Trigger.XStorable.Model;
using System.Collections.Generic;
using System.Linq;
using Trigger.XStorable.DataStore;

namespace Trigger.BCL.ParaOffice
{

    [System.ComponentModel.DefaultProperty("Art")]
    public class AktArt : StorableBase
    {
        public string Art
        {
            get;
            set;
        }

        public string IdText
        {
            get;
            set;
        }

        public string Bemerkung
        {
            get;
            set;
        }

        [System.ComponentModel.DisplayName("Akten zu Aktart")]
        [System.Runtime.Serialization.IgnoreDataMember]
        [LinkedList(typeof(Akt))]
        public IEnumerable<Akt> LinkedAkten
        {
            get
            {
                return Store.LoadAll<Akt>().Where(p => p.AktArt != null && p.AktArt.MappingId == this.MappingId);
            }
        }
    }
}
