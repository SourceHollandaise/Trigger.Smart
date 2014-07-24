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
    [CompactViewRepresentation]
    [MainViewItem]
    public class Person : StorableBase
    {
        [System.ComponentModel.DisplayName("Person")]
        [VisibleOnView(TargetView.None)]
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
        [VisibleOnView(TargetView.ListOnly)]
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

        [System.ComponentModel.DisplayName("Vorname (Name1)")]
        [VisibleOnView(TargetView.DetailOnly)]
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
        [VisibleOnView(TargetView.DetailOnly)]
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
        [VisibleOnView(TargetView.DetailOnly)]
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
        [VisibleOnView(TargetView.DetailOnly)]
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
    }
}
