using System;
using System.Linq;
using XForms.Model;
using XForms.Store;

namespace Trigger.BCL.ParaOffice
{
    [System.ComponentModel.DefaultProperty("IdText")]
    [System.ComponentModel.DisplayName("Leistungskatalog (Sparte)")]
    public class LeistungKatalog : ExportableBase
    {
        string idText;

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

        string beschreibung;

        public string Beschreibung
        {
            get
            {
                return beschreibung;
            }
            set
            {
                if (Equals(beschreibung, value))
                    return;
                beschreibung = value;

                OnPropertyChanged();
            }
        }
    }
    
}
