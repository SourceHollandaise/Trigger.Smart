using Trigger.XStorable.DataStore;
using System.Collections.Generic;
using System.Linq;
using Trigger.XStorable.Model;

namespace Trigger.BCL.EventTracker.Model
{
    [System.ComponentModel.DefaultProperty("DisplayName")]
    [ViewCompact]
    [ViewNavigation]
    public class Person : StorableBase
    {
        [System.ComponentModel.DisplayName("Person")]
        [FieldVisible(TargetView.None)]
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

        [FieldVisible(TargetView.ListOnly)]
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

        [FieldGroup("Name", 1, 1)]
        [System.ComponentModel.DisplayName("First name")]
        [FieldVisible(TargetView.DetailOnly)]
        [FieldLabelBehaviour(false, "First name")]
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

        [FieldGroup("Name", 1, 2)]
        [System.ComponentModel.DisplayName("Middle name")]
        [FieldVisible(TargetView.DetailOnly)]
        [FieldLabelBehaviour(false, "Middle name")]
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

        [FieldGroup("Name", 1, 3)]
        [System.ComponentModel.DisplayName("Last name")]
        [FieldVisible(TargetView.DetailOnly)]
        [FieldLabelBehaviour(false, "Last name")]
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

        [FieldGroup("Address", 2, 1)]
        [FieldLabelBehaviour(false, "Postal code")]
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

        [FieldGroup("Address", 2, 2)]
        [FieldLabelBehaviour(false, "City")]
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

        [FieldGroup("Address", 2, 3)]
        [FieldLabelBehaviour(false, "Street")]
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

        [FieldGroup("Links", 3, 1)]
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
