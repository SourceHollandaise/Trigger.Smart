using Trigger.XStorable.Model;
using Trigger.XStorable.DataStore;

namespace Trigger.BCL.ParaOffice
{

    [System.ComponentModel.DefaultProperty("Subject")]
    [System.ComponentModel.DisplayName("Dokument")]
    [ViewCompact]
    [ViewNavigation]
    public class Dokument : StorableBase, IFileData
    {
        public override void Initialize()
        {
            SK = CurrentSBService.CurrentSB;
        }

        [System.ComponentModel.DisplayName("Datei")]
        [FieldVisible(TargetView.None)]
        public override string GetRepresentation
        {
            get
            {
                var sb = new System.Text.StringBuilder();
                sb.AppendLine(string.Format("'{0}' by {1}", Subject, SK != null ? SK.ID : "Kein"));
                sb.AppendLine(string.Format("Zu Akt '{0}'", AktAlias));
                sb.AppendLine(string.Format("{0}", Bemerkung));
                return sb.ToString();
            }
        }

        string subject;

        [FieldGroup("Datei", 1, 1)]
        [System.ComponentModel.DisplayName("Bezeichnung")]
        public string Subject
        {
            get
            {
                return subject;
            }
            set
            {
                if (Equals(subject, value))
                    return;
                subject = value;

                OnPropertyChanged();
            }
        }

       

        SB ra;

        [FieldGroup("Zuweisung", 2, 1)]
        [LinkedObject]
        public SB RA
        {
            get
            {
                return ra;
            }
            set
            {
                if (Equals(ra, value))
                    return;
                ra = value;

                OnPropertyChanged();
            }
        }

        SB sk;

        [FieldGroup("Zuweisung", 2, 2)]
        [LinkedObject]
        public SB SK
        {
            get
            {
                return sk;
            }
            set
            {
                if (Equals(sk, value))
                    return;
                sk = value;

                OnPropertyChanged();
            }
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

        [FieldGroup("Zuweisung", 2, 3)]
        [LinkedObject]
        [FieldVisible(TargetView.DetailOnly)]
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

                if (RA == null && akt != null)
                    RA = akt.SB1;
            }
        }

        DokumentArt art;

        [FieldGroup("Status", 3, 1)]
        public DokumentArt Art
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

        DokumentMedium medium;

        [FieldGroup("Status", 3, 2)]
        public DokumentMedium Medium
        {
            get
            {
                return medium;
            }
            set
            {
                if (Equals(medium, value))
                    return;
                medium = value;

                OnPropertyChanged();
            }
        }


        DokumentStatus status;

        [FieldGroup("Status", 3, 3)]
        public DokumentStatus Status
        {
            get
            {
                return status;
            }
            set
            {
                if (Equals(status, value))
                    return;
                status = value;

                OnPropertyChanged();
            }
        }

        string bemerkung;

        [FieldGroup("Sonstige Informationen", 4, 1)]
        [System.ComponentModel.DisplayName("Bemerkung")]
        [FieldVisible(TargetView.DetailOnly)]
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

        string fileName;

        [FieldGroup("Vorschau", 5, 1)]
        [System.ComponentModel.DisplayName("Datei")]
        [FieldVisible(TargetView.DetailOnly)]
        [System.ComponentModel.ReadOnly(true)]
        [FileData]
        public string FileName
        {
            get
            {
                return fileName;
            }
            set
            {
                if (Equals(fileName, value))
                    return;
                fileName = value;

                OnPropertyChanged();
            }
        }
    }
}
