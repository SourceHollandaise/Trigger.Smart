using System.Collections.Generic;
using System.Linq;
using XForms.Model;
using XForms.Store;

namespace Trigger.BCL.EventTracker.Model
{
    [System.ComponentModel.DefaultProperty("PersonDisplayName")]
    [System.ComponentModel.DisplayName("Person")]
    [ImageName("user")]
    public class Person : StorableBase
    {
        public string PersonDisplayName
        {
            get
            {
                var displayName = FirstName + " " + LastName;

                if (!string.IsNullOrWhiteSpace(Company))
                    displayName += " (" + Company + ")";

                return displayName;
            }
        }

        public string AddressDisplayName
        {
            get
            {
                return PostalCode + " " + City + " " + Street;
            }
        }

        string firstName;

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

        string company;

        public string Company
        {
            get
            {
                return company;
            }
            set
            {
                if (Equals(company, value))
                    return;
                company = value;

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
