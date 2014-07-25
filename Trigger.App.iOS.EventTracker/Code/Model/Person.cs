using Trigger.XStorable.DataStore;
using System.Collections.Generic;
using System.Linq;
using Trigger.XStorable.Model;

namespace Trigger.BCL.EventTracker.Model
{
    [System.ComponentModel.DefaultProperty("DisplayName")]
    [CompactViewRepresentationAttribute]
    [MainViewItem]
    public class Person : StorableBase
    {
        [System.ComponentModel.DisplayName("Person")]
        [VisibleOnView(TargetView.None)]
        public override string GetRepresentation
        {
            get
            {
                var sb = new System.Text.StringBuilder();
                sb.AppendLine(string.Format("{0} {1}", FirstName, LastName));
                sb.AppendLine(string.Format("{0} {1} - {2}", PostalCode, City, Street));
                return sb.ToString();
            }
        }

        string displayName;

        [VisibleOnView(TargetView.ListOnly)]
        public string DisplayName
        {
            get
            {
                displayName = FirstName + " " + LastName;
                return displayName;
            }
            set
            {
                if (Equals(displayName, value))
                    return;
                displayName = value;

                OnPropertyChanged();
            }
        }

        string firstName;

        [System.ComponentModel.DisplayName("First name")]
        [VisibleOnView(TargetView.DetailOnly)]
        public string FirstName
        {
            get
            {
                return firstName;
            }
            set
            {
                if (Equals(firstName, value))
                    return;
                firstName = value;

                OnPropertyChanged();
            }
        }

        string middleName;

        [System.ComponentModel.DisplayName("Middle name")]
        [VisibleOnView(TargetView.DetailOnly)]
        public string MiddleName
        {
            get
            {
                return middleName;
            }
            set
            {
                if (Equals(middleName, value))
                    return;
                middleName = value;

                OnPropertyChanged();
            }
        }

        string lastName;

        [System.ComponentModel.DisplayName("Last name")]
        [VisibleOnView(TargetView.DetailOnly)]
        public string LastName
        {
            get
            {
                return lastName;
            }
            set
            {
                if (Equals(lastName, value))
                    return;
                lastName = value;

                OnPropertyChanged();
            }
        }

        string postalCode;

        public string PostalCode
        {
            get
            {
                return postalCode;
            }
            set
            {
                if (Equals(postalCode, value))
                    return;
                postalCode = value;

                OnPropertyChanged();
            }
        }

        string city;

        public string City
        {
            get
            {
                return city;
            }
            set
            {
                if (Equals(city, value))
                    return;
                city = value;

                OnPropertyChanged();
            }
        }

        string street;

        public string Street
        {
            get
            {
                return street;
            }
            set
            {
                if (Equals(street, value))
                    return;
                street = value;

                OnPropertyChanged();
            }
        }

        [System.ComponentModel.DisplayName("Linked contacts")]
        [System.Runtime.Serialization.IgnoreDataMember]
        [LinkedList(typeof(Contact))]
        public IEnumerable<Contact> LinkedContacts
        {
            get
            {
                return Store.LoadAll<Contact>().Where(p => p.Person != null && p.Person.MappingId.Equals(MappingId));
            }
        }
    }
}
