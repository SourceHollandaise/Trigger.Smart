using System.Collections.Generic;
using System.Linq;
using XForms.Model;
using XForms.Store;

namespace Trigger.BCL.ParaOffice
{
    [System.ComponentModel.DefaultProperty("ID")]
    [System.ComponentModel.DisplayName("Sachbearbeiter")]
    [ImageName("user_accept")]
    public class SB : ExportableBase
    {
        string id;

        [System.ComponentModel.DisplayName("SB-Kürzel")]
        public string ID
        {
            get
            {
                return id;
            }
            set
            {
                if (Equals(id, value))
                    return;
                id = value;

                OnPropertyChanged();
            }
        }


        public string UserAlias
        {
            get
            {
                return User != null ? User.UserName : null;
            }
        }

        User user;

        [LinkedObject]
        public User User
        {
            get
            {
                return user;
            }
            set
            {
                if (Equals(user, value))
                    return;
                user = value;

                OnPropertyChanged();
            }
        }

        bool termineAnzeigen;

        public bool TermineAnzeigen
        {
            get
            {
                return termineAnzeigen;
            }
            set
            {
                if (Equals(termineAnzeigen, value))
                    return;
                termineAnzeigen = value;

                OnPropertyChanged();
            }
        }

        bool telefonatAnzeigen;

        public bool TelefonatAnzeigen
        {
            get
            {
                return telefonatAnzeigen;
            }
            set
            {
                if (Equals(telefonatAnzeigen, value))
                    return;
                telefonatAnzeigen = value;

                OnPropertyChanged();
            }
        }

        bool aktiv;

        public bool Aktiv
        {
            get
            {
                return aktiv;
            }
            set
            {
                if (Equals(aktiv, value))
                    return;
                aktiv = value;

                OnPropertyChanged();
            }
        }

        [System.ComponentModel.DisplayName("Termine von SB")]
        [System.Runtime.Serialization.IgnoreDataMember]
        [LinkedList(typeof(Termin))]
        public IEnumerable<Termin> LinkedTermine
        {
            get
            {
                return Store.LoadAll<Termin>().Where(p => p.SB != null && p.SB.ID.Equals(ID));
            }
        }


        [System.ComponentModel.DisplayName("Telefonate für SB")]
        [System.Runtime.Serialization.IgnoreDataMember]
        [LinkedList(typeof(Telefonat))]
        public IEnumerable<Telefonat> LinkedTelefonate
        {
            get
            {
                return Store.LoadAll<Telefonat>().Where(p => p.SB2 != null && p.SB2.ID.Equals(ID));
            }
        }

        [System.ComponentModel.DisplayName("Akten von SB")]
        [System.Runtime.Serialization.IgnoreDataMember]
        [LinkedList(typeof(Akt))]
        public IEnumerable<Akt> LinkedAkten
        {
            get
            {
                return Store.LoadAll<Akt>().Where(p => p.SB1 != null && p.SB1.ID.Equals(ID));
            }
        }
    }
}
