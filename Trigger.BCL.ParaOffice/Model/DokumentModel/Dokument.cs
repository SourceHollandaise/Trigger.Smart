using Trigger.XStorable.Model;
using Trigger.XStorable.DataStore;

namespace Trigger.BCL.ParaOffice
{

    [System.ComponentModel.DefaultProperty("Subject")]
    [System.ComponentModel.DisplayName("Dokument")]
    [CompactViewItem]
    [MainViewItem]
    public class Dokument : StorableBase, IFileData
    {
        public override void Initialize()
        {
            SK = CurrentSBService.CurrentSB;
        }

        [System.ComponentModel.DisplayName("Datei")]
        [VisibleOnView(TargetView.None)]
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

        [InGroup("Datei", 1, 1)]
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

        [InGroup("Zuweisung", 2, 1)]
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

        [InGroup("Zuweisung", 2, 2)]
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
        [VisibleOnView(TargetView.ListOnly)]
        public string AktAlias
        {
            get
            {
                return Akt != null ? Akt.Bezeichnung : null;
            }
        }

        Akt akt;

        [InGroup("Zuweisung", 2, 3)]
        [LinkedObject]
        [VisibleOnView(TargetView.DetailOnly)]
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

        [InGroup("Status", 3, 1)]
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

        [InGroup("Status", 3, 2)]
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

        [InGroup("Status", 3, 3)]
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

        [InGroup("Sonstige Informationen", 4, 1)]
        [System.ComponentModel.DisplayName("Bemerkung")]
        [VisibleOnView(TargetView.DetailOnly)]
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

        [InGroup("Vorschau", 5, 1)]
        [System.ComponentModel.DisplayName("Datei")]
        [VisibleOnView(TargetView.DetailOnly)]
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
