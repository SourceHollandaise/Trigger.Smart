using Trigger.XStorable.Model;
using Trigger.XStorable.DataStore;

namespace Trigger.BCL.ParaOffice
{

    [System.ComponentModel.DefaultProperty("Subject")]
    [System.ComponentModel.DisplayName("Dokument")]
    [CompactViewRepresentation]
    [MainViewItem]
    public class Dokument : StorableBase, IFileData
    {
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
                //sb.AppendLine(string.Format("ID: {0}", MappingId));
                return sb.ToString();
            }
        }

        string subject;

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

        string fileName;

        [System.ComponentModel.DisplayName("Datei")]
        [VisibleOnView(TargetView.DetailOnly)]
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

        SB ra;

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
            }
        }

        DokumentArt art;

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
    }
}
