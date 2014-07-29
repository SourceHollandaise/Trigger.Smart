using Trigger.XStorable.Model;
using Trigger.XStorable.DataStore;

namespace Trigger.BCL.ParaOffice
{

    [System.ComponentModel.DefaultProperty("Akt")]
    [System.ComponentModel.DisplayName("Person zu Akt")]
    [ViewCompact]
    [ViewNavigation]
    public class AktPerson : StorableBase
    {
        [System.ComponentModel.DisplayName("Person")]
        [FieldVisible(TargetView.None)]
        public override string GetRepresentation
        {
            get
            {
                var sb = new System.Text.StringBuilder();
                sb.AppendLine(string.Format("{0} zu Akt {1}", PersonAlias, AktAlias));
                sb.AppendLine(string.Format("{0} mit Nummer {1}", Partei, Reihung));
                return sb.ToString();
            }
        }

        public override void Initialize()
        {
            base.Initialize();

            Streitgenosse = true;
            Partei = Partei.Partei1;
        }

        [System.ComponentModel.DisplayName("Akt")]
        [System.Runtime.Serialization.IgnoreDataMember]
        [FieldVisible(TargetView.ListOnly)]
        public string AktAlias
        {
            get
            {
                return Akt != null ? Akt.Bezeichnung : null;
            }
        }

        Akt akt;

        [LinkedObject]
        [FieldVisible(TargetView.DetailOnly)]
        [FieldGroup("Verbindung", 1, 1)]
        public Akt Akt
        {
            get
            {
                return akt;
            }
            set
            {
                if (Equals(akt, value))
                    return;
                akt = value;

                OnPropertyChanged();
            }
        }

        [System.ComponentModel.DisplayName("Person")]
        [FieldVisible(TargetView.ListOnly)]
        public string PersonAlias
        {
            get
            {
                return Person != null ? (Person.Vorname + " " + Person.Nachname) : null;
            }
        }

        Person person;

        [System.ComponentModel.DisplayName("Person")]
        [LinkedObject]
        [FieldVisible(TargetView.DetailOnly)]
        [FieldGroup("Verbindung", 1, 2)]
        public Person Person
        {
            get
            {
                return person;
            }
            set
            {
                if (Equals(person, value))
                    return;
                person = value;

                OnPropertyChanged();
            }
        }

        [System.ComponentModel.DisplayName("Vertreter")]
        [FieldVisible(TargetView.ListOnly)]
        public string VertreterAlias
        {
            get
            {
                return Person != null && Vertreter != null ? Vertreter.Vorname + " " + Vertreter.Nachname : null;
            }
        }

        Person vertreter;

        [System.ComponentModel.DisplayName("Vertreter")]
        [LinkedObject]
        [FieldVisible(TargetView.DetailOnly)]
        [FieldGroup("Vetretung", 2, 1)]
        public Person Vertreter
        {
            get
            {
                return vertreter;
            }
            set
            {
                if (Equals(vertreter, value))
                    return;
                vertreter = value;

                OnPropertyChanged();
            }
        }

        Partei partei;

        [FieldGroup("Erweitert", 3, 1)]
        [System.ComponentModel.DisplayName("Klient/Gegner")]
        public Partei Partei
        {
            get
            {
                return partei;
            }
            set
            {
                if (Equals(partei, value))
                    return;
                partei = value;

                OnPropertyChanged();
            }
        }

        bool streitgenosse;

        [FieldGroup("Erweitert", 3, 2)]
        public bool Streitgenosse
        {
            get
            {
                return streitgenosse;
            }
            set
            {
                if (Equals(streitgenosse, value))
                    return;
                streitgenosse = value;

                OnPropertyChanged();
            }
        }

        int reihung;

        [FieldGroup("Erweitert", 3, 3)]
        public int Reihung
        {
            get
            {
                return reihung;
            }
            set
            {
                if (Equals(reihung, value))
                    return;
                reihung = value;

                OnPropertyChanged();
            }
        }
    }
}