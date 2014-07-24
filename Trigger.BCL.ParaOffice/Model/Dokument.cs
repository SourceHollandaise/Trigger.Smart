using System;
using Trigger.XStorable.Model;
using System.Collections.Generic;
using System.Linq;
using Trigger.XStorable.DataStore;
using Trigger.BCL.Common.Model;

namespace Trigger.BCL.ParaOffice
{
    public class Dokument : StorableBase, IFileData
    {
        [System.ComponentModel.DisplayName("Bezeichnung")]
        public string Subject
        {
            get;
            set;
        }

        [System.ComponentModel.DisplayName("Datei")]
        public string FileName
        {
            get;
            set;
        }

        public SB RA
        {
            get;
            set;
        }

        public SB SK
        {
            get;
            set;
        }

        public Akt Akt
        {
            get;
            set;
        }
    }
    
}
