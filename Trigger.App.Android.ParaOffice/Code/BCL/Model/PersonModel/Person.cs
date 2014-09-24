using System.Collections.Generic;
using System.Linq;
using XForms.Model;
using XForms.Store;

namespace Trigger.BCL.ParaOffice
{
  
    [System.ComponentModel.DefaultProperty("PersonenName")]
    [System.ComponentModel.DisplayName("Person")]
    [ImageName("businesspeople")]
    public class Person : ExportableBase
    {
        public override string GetSearchString()
        {
            return PersonenName + PLZ + Ort + Strasse;
        }

        public override string GetRepresentation
        {
            get
            {
                return PersonenName;
            }
        }

        [System.ComponentModel.DisplayName("Personenname")]
        public string PersonenName
        {
            get
            {
                return Vorname + " " + Nachname + " " + Titel;
            }
        }

        string vorname;

        [System.ComponentModel.DisplayName("Vorname (Name1)")]
        public string Vorname
        {
            get
            {
                return vorname;
            }
            set
            {
                if (Equals(vorname, value))
                    return;
                vorname = value;

                OnPropertyChanged();
            }
        }

        string nachname;

        [System.ComponentModel.DisplayName("Nachname (Name2)")]
        public string Nachname
        {
            get
            {
                return nachname;
            }
            set
            {
                if (Equals(nachname, value))
                    return;
                nachname = value;

                OnPropertyChanged();
            }
        }

        string titel;

        [System.ComponentModel.DisplayName("Titel")]
        public string Titel
        {
            get
            {
                return titel;
            }
            set
            {
                if (Equals(titel, value))
                    return;
                titel = value;

                OnPropertyChanged();
            }
        }

        string anrede;

        [System.ComponentModel.DisplayName("Anrede")]
        public string Anrede
        {
            get
            {
                return anrede;
            }
            set
            {
                if (Equals(anrede, value))
                    return;
                anrede = value;

                OnPropertyChanged();
            }
        }

        PersonenArt art;

        public PersonenArt Art
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

        string plz;

        public string PLZ
        {
            get
            {
                return plz;
            }
            set
            {
                if (Equals(plz, value))
                    return;
                plz = value;

                OnPropertyChanged();
            }
        }

        string ort;

        public string Ort
        {
            get
            {
                return ort;
            }
            set
            {
                if (Equals(ort, value))
                    return;
                ort = value;

                OnPropertyChanged();
            }
        }

        string strasse;

        public string Strasse
        {
            get
            {
                return strasse;
            }
            set
            {
                if (Equals(strasse, value))
                    return;
                strasse = value;

                OnPropertyChanged();
            }
        }

        [System.ComponentModel.DisplayName("Kontakte zu Person")]
        [System.Runtime.Serialization.IgnoreDataMember]
        [LinkedList(typeof(Kontakt))]
        public IEnumerable<Kontakt> LinkedKontakte
        {
            get
            {
                return Store.LoadAll<Kontakt>().Where(p => p.Person != null && p.Person.MappingId.Equals(MappingId));
            }
        }

        [System.ComponentModel.DisplayName("Akten zu Person")]
        [System.Runtime.Serialization.IgnoreDataMember]
        [LinkedList(typeof(Akt))]
        public IEnumerable<Akt> LinkedAkten
        {
            get
            {
                return Store.LoadAll<AktPerson>().Where(p => p.Person != null && p.Person.MappingId.Equals(MappingId)).Select(p => p.Akt);
            }
        }
    }
}
