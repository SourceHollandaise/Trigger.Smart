using XForms.Store;
using XForms.Model;

namespace Trigger.BCL.ParaOffice
{

    [System.ComponentModel.DefaultProperty("Subject")]
    [System.ComponentModel.DisplayName("ERV-Rückverkehr-Anhang")]
    public class ErvRueckverkehrDokument : ExportableBase, IFileData
    {
        public override string GetSearchString()
        {
            return Subject + FileName;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        string subject;

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
        [FieldFileData]
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

        ErvRueckverkehr rueckverkehr;

        [LinkedObject]
        public ErvRueckverkehr Rueckverkehr
        {
            get
            {
                return rueckverkehr;
            }
            set
            {
                if (Equals(rueckverkehr, value))
                    return;
                rueckverkehr = value;

                OnPropertyChanged();
            }
        }
    }
}
