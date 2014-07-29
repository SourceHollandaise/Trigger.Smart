using System;
using Trigger.XStorable.Model;
using System.Collections.Generic;
using System.Linq;
using Trigger.XStorable.DataStore;
using Trigger.BCL.Common.Model;

namespace Trigger.BCL.ParaOffice
{
  
    [System.ComponentModel.DefaultProperty("PersonenName")]
    [System.ComponentModel.DisplayName("Person")]
    [ViewCompact]
    [ViewNavigation]
    public class Person : StorableBase
    {
        [System.ComponentModel.DisplayName("Person")]
        [FieldVisible(TargetView.None)]
        public override string GetRepresentation
        {
            get
            {
                var sb = new System.Text.StringBuilder();
                sb.AppendLine(string.Format("{0} {1} {2}", Vorname, Nachname, Titel));
                sb.AppendLine(string.Format("{0} {1} - {2}", PLZ, Ort, Strasse));
                return sb.ToString();
            }
        }

        string personenName;

      
        [System.ComponentModel.DisplayName("Personenname")]
        [FieldVisible(TargetView.ListOnly)]
        public string PersonenName
        {
            get
            {
                personenName = Vorname + " " + Nachname;
                return personenName;
            }
            set
            {
                if (Equals(personenName, value))
                    return;
                personenName = value;

                OnPropertyChanged();
            }
        }

        string vorname;

        [FieldGroup("Name", 1, 1)]
        [System.ComponentModel.DisplayName("Vorname (Name1)")]
        [FieldVisible(TargetView.DetailOnly)]
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

        [FieldGroup("Name", 1, 2)]
        [System.ComponentModel.DisplayName("Nachname (Name2)")]
        [FieldVisible(TargetView.DetailOnly)]
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

        [FieldGroup("Name", 1, 3)]
        [System.ComponentModel.DisplayName("Titel")]
        [FieldVisible(TargetView.DetailOnly)]
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

        [FieldGroup("Name", 1, 3)]
        [System.ComponentModel.DisplayName("Anrede")]
        [FieldVisible(TargetView.DetailOnly)]
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

        [FieldGroup("Person", 2, 1)]
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

        [FieldGroup("Adresse", 3, 1)]
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

        [FieldGroup("Adresse", 3, 2)]
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

        [FieldGroup("Adresse", 3, 3)]
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

        [FieldGroup("Verknüpfungen", 4, 1)]
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

        [FieldGroup("Verknüpfungen", 4, 2)]
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
