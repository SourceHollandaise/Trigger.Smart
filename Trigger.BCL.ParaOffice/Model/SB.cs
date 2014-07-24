using System;
using Trigger.XStorable.Model;
using System.Collections.Generic;
using System.Linq;
using Trigger.XStorable.DataStore;
using Trigger.BCL.Common.Model;

namespace Trigger.BCL.ParaOffice
{
    [System.ComponentModel.DefaultProperty("ID")]
    public class SB : StorableBase
    {
        public string ID
        {
            get;
            set;
        }

        public User User
        {
            get;
            set;
        }
    }
    
}
