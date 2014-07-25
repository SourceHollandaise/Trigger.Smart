using System;
using Trigger.XStorable.Model;
using System.Collections.Generic;
using System.Linq;
using Trigger.XStorable.DataStore;
using Trigger.BCL.Common.Model;

namespace Trigger.BCL.ParaOffice
{
    [System.ComponentModel.DefaultProperty("ID")]
    [System.ComponentModel.DisplayName("Sachbearbeiter")]
    [CompactViewRepresentationAttribute]
    public class SB : StorableBase
    {
        [System.ComponentModel.DisplayName("SB")]
        [VisibleOnView(TargetView.None)]
        public override string GetRepresentation
        {
            get
            {
                return ID;
            }
        }

        string id;

        [System.ComponentModel.DisplayName("SB-Kürzel")]
        public string ID
        {
            get
            {
                return id;
            }
            set
            {
                if (Equals(id, value))
                    return;
                id = value;

                OnPropertyChanged();
            }
        }

        User user;

        [LinkedObject]
        public User User
        {
            get
            {
                return user;
            }
            set
            {
                if (Equals(user, value))
                    return;
                user = value;

                OnPropertyChanged();
            }
        }
    }
}
