using Trigger.XStorable.DataStore;
using Trigger.XStorable.Model;

namespace Trigger.BCL.EventTracker.Model
{

    [System.ComponentModel.DefaultProperty("Person")]
    [ViewCompact]
    [ViewNavigation]
    public class Contact : StorableBase
    {
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
