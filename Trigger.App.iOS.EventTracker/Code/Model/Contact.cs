using Trigger.XStorable.DataStore;
using Trigger.XStorable.Model;
using Trigger.BCL.Common.Model;

namespace Trigger.BCL.EventTracker.Model
{

    [System.ComponentModel.DefaultProperty("PhoneNumber")]
    [CompactViewRepresentationAttribute]
    [ViewNavigation]
    public class Contact : StorableBase
    {
        [System.ComponentModel.DisplayName("Contact")]
        [FieldVisible(TargetView.None)]
        public override string GetRepresentation
        {
            get
            {
                var sb = new System.Text.StringBuilder();
                sb.AppendLine(string.Format("{0}", PersonAlias));
                if (!string.IsNullOrEmpty(PhoneNumber))
                    sb.AppendLine(string.Format("Phone: {0}", PhoneNumber));
                if (!string.IsNullOrEmpty(MobileNumber))
                    sb.AppendLine(string.Format("Mobile: {0}", MobileNumber));
                if (!string.IsNullOrEmpty(Email))
                    sb.AppendLine(string.Format("E-Mail: {0}", Email));
                return sb.ToString();
            }
        }

        [System.ComponentModel.DisplayName("Person")]
        [FieldVisible(TargetView.ListOnly)]
        public string PersonAlias
        {
            get
            {
                return Person != null ? (Person.FirstName + " " + Person.LastName) : null;
            }
        }

        Person person;

        [System.ComponentModel.DisplayName("Person")]
        [LinkedObject]
        [FieldVisible(TargetView.DetailOnly)]
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