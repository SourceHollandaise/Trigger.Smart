using XForms.Model;
using XForms.Store;

namespace Trigger.BCL.EventTracker.Model
{
    [System.ComponentModel.DefaultProperty("PersonAlias")]
    [System.ComponentModel.DisplayName("Contact")]
    [ImageName("address_book2")]
    public class Contact : StorableBase
    {
        public override string GetRepresentation
        {
            get
            {
                return PersonAlias + " - " + PhoneNumber + " " + MobileNumber + " " + Email;
            }
        }

        public override string GetSearchString()
        {
            return PersonAlias + PhoneNumber + MobileNumber + Email;
        }

        public string PersonAlias
        {
            get
            {
                return Person != null ? Person.PersonDisplayName : null;
            }
        }

        Person person;

        [System.ComponentModel.DisplayName("Person")]
        [LinkedObject]
        public Person Person
        {
            get
            {
                return person;
            }
            set
            {
                if (Equals(person, value))
                    return;
                person = value;

                OnPropertyChanged();
            }
        }

        ContactType contactType;

        [System.ComponentModel.DisplayName("Contact")]
        public ContactType ContactType
        {
            get
            {
                return contactType;
            }
            set
            {
                if (Equals(contactType, value))
                    return;
                contactType = value;

                OnPropertyChanged();
            }
        }

        string phoneNumber;

        [System.ComponentModel.DisplayName("Phone")]
        public string PhoneNumber
        {
            get
            {
                return phoneNumber;
            }
            set
            {
                if (Equals(phoneNumber, value))
                    return;
                phoneNumber = value;

                OnPropertyChanged();
            }
        }

        string mobileNumber;

        [System.ComponentModel.DisplayName("Mobile")]
        public string MobileNumber
        {
            get
            {
                return mobileNumber;
            }
            set
            {
                if (Equals(mobileNumber, value))
                    return;
                mobileNumber = value;

                OnPropertyChanged();
            }
        }

        string email;

        [System.ComponentModel.DisplayName("E-Mail")]
        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                if (Equals(email, value))
                    return;
                email = value;

                OnPropertyChanged();
            }
        }
    }
}
