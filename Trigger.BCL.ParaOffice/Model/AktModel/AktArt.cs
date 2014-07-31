using System.Collections.Generic;
using System.Linq;
using Trigger.XStorable.DataStore;
using Trigger.BCL.Common.Datastore;

namespace Trigger.BCL.ParaOffice
{

    [System.ComponentModel.DefaultProperty("Art")]
    [System.ComponentModel.DisplayName("Aktart")]
    public class AktArt : StorableBase
    {
        string art;

        public string Art
        {
            get
            {
                return art;
            }
            set
            {
                if (Equals(art, value))
                    return;
                art = value;

                OnPropertyChanged();
            }
        }

        string idText;

        [System.ComponentModel.DisplayName("ID-Text")]
        public string IdText
        {
            get
            {
                return idText;
            }
            set
            {
                if (Equals(idText, value))
                    return;
                idText = value;

                OnPropertyChanged();
            }
        }

        string bemerkung;

        public string Bemerkung
        {
            get
            {
                return bemerkung;
            }
            set
            {
                if (Equals(bemerkung, value))
                    return;
                bemerkung = value;

                OnPropertyChanged();
            }
        }

        [System.ComponentModel.DisplayName("Akten zu Aktart")]
        [System.Runtime.Serialization.IgnoreDataMember]
        [LinkedList(typeof(Akt))]
        public IEnumerable<Akt> LinkedAkten
        {
            get
            {
                return Store.LoadAll<Akt>().Where(p => p.AktArt != null && p.AktArt.MappingId.Equals(MappingId));
            }
        }
    }
}
