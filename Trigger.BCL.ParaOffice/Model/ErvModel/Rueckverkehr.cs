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
            return base.GetSearchString();
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public string ErvCode
        {
            get;
            set;
        }

        public string Art
        {
            get;
            set;
        }

        public int AnzahlDokumentAnhang
        {
            get;
            set;
        }

        public DateTime EmpfangDatum
        {
            get;
            set;
        }

        public DateTime HinterlegungDatum
        {
            get;
            set;
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

    [System.ComponentModel.DefaultProperty("Subject")]
    [System.ComponentModel.DisplayName("Rückverkehr-Anhang")]
    public class RueckverkehrDokument : ExportableBase, IFileData
    {
        public override string GetSearchString()
        {
            return base.GetSearchString();
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public string Subject
        {
            get;
            set;
        }

        public string FileName
        {
            get;
            set;
        }

        public Rueckverkehr Rueckverkehr
        {
            get;
            set;
        }
    }
}
