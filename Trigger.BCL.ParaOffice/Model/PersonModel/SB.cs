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
    [ViewCompact]
    [ViewNavigation]
    public class SB : StorableBase
    {
        [System.ComponentModel.DisplayName("SB")]
        [FieldVisible(TargetView.None)]
        public override string GetRepresentation
        {
            get
            {
                return ID;
            }
        }

        string id;

        [FieldGroup("Sachbearbeiter / User", 1, 1)]
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

        [FieldGroup("Sachbearbeiter / User", 1, 2)]
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

        [FieldGroup("Verknüpfungen", 2, 1)]
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

        [FieldGroup("Verknüpfungen", 2, 2)]
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
