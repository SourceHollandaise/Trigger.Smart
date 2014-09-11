using System;
using XForms.Store;
using System.Collections.Generic;
using System.Linq;

namespace Trigger.BCL.ParaOffice
{
    [System.ComponentModel.DefaultProperty("ErvCode")]
    [System.ComponentModel.DisplayName("Rückverkehr")]
    public class Rueckverkehr : ExportableBase
    {
        public override string GetSearchString()
        {
            return ErvCode + Art + EmpfangDatum.ToLongDateString() + HinterlegungDatum.ToLongDateString();
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        string ervCode;

        public string ErvCode
        {
            get
            {
                return ervCode;
            }
            set
            {
                if (Equals(ervCode, value))
                    return;
                ervCode = value;

                OnPropertyChanged();
            }
        }

        string art;

        //TODO: Enum?
        public string Art
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

        int anzahlDokumentAnhang;

        public int AnzahlDokumentAnhang
        {
            get
            {
                return anzahlDokumentAnhang;
            }
            set
            {
                if (Equals(anzahlDokumentAnhang, value))
                    return;
                anzahlDokumentAnhang = value;

                OnPropertyChanged();
            }
        }

        DateTime empfangDatum;

        public DateTime EmpfangDatum
        {
            get
            {
                return empfangDatum;
            }
            set
            {
                if (Equals(empfangDatum, value))
                    return;
                empfangDatum = value;

                OnPropertyChanged();
            }
        }

        DateTime hinterlegungDatum;

        public DateTime HinterlegungDatum
        {
            get
            {
                return hinterlegungDatum;
            }
            set
            {
                if (Equals(hinterlegungDatum, value))
                    return;
                hinterlegungDatum = value;

                OnPropertyChanged();
            }
        }

        [System.ComponentModel.DisplayName("Dokumente zu Rückverkehr")]
        [System.Runtime.Serialization.IgnoreDataMember]
        [LinkedList(typeof(RueckverkehrDokument))]
        public IEnumerable<RueckverkehrDokument> LinkedRueckverkehrDokumente
        {
            get
            {
                return Store.LoadAll<RueckverkehrDokument>().Where(p => p.Rueckverkehr != null && p.Rueckverkehr.MappingId.Equals(MappingId));
            }
        }
    }
}
