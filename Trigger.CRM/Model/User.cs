using System.ComponentModel;

namespace Trigger.CRM.Model
{
    public class User : ModelBase
    {
        string userName;

        public string UserName
        {
            get
            {
                return userName;
            }
            set
            {
                if (Equals(userName, value))
                    return;
                userName = value;

                OnPropertyChanged(new PropertyChangedEventArgs("UserName"));
            }
        }

        string password;

        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                if (Equals(password, value))
                    return;
                password = value;

                OnPropertyChanged(new PropertyChangedEventArgs("Password"));
            }
        }

        string eMail;

        public string EMail
        {
            get
            {
                return eMail;
            }
            set
            {
                if (Equals(eMail, value))
                    return;
                eMail = value;

                OnPropertyChanged(new PropertyChangedEventArgs("EMail"));
            }
        }
    }
}
