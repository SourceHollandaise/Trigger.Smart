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
    [MainViewItem]
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

        [System.ComponentModel.DisplayName("Termine zu SB")]
        [System.Runtime.Serialization.IgnoreDataMember]
        [LinkedList(typeof(Termin))]
        public IEnumerable<Termin> LinkedTermine
        {
            get
            {
                return Store.LoadAll<Termin>().Where(p => p.SB != null && p.SB.MappingId.Equals(MappingId));
            }
        }

        [System.ComponentModel.DisplayName("Akten zu SB")]
        [System.Runtime.Serialization.IgnoreDataMember]
        [LinkedList(typeof(Akt))]
        public IEnumerable<Akt> LinkedAkten
        {
            get
            {
                return Store.LoadAll<Akt>().Where(p => p.SB1 != null && p.SB1.MappingId.Equals(MappingId));
            }
        }
    }
}
