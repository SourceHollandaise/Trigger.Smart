using Trigger.BCL.Common.Datastore;

namespace Trigger.BCL.Common.Model
{
    public enum Sex
    {
        Female,
        Male
    }


    [System.ComponentModel.DefaultProperty("UserName")]
    public class User : StorableBase
    {
        public override void Initialize()
        {
            //INFO: Do not initialize!!! 
        }

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

                OnPropertyChanged();
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
    
                OnPropertyChanged();
            }
        }

        string email;

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

        Sex userSex;

        public Sex UserSex
        {
            get
            {
                return userSex;
            }
            set
            {
                if (Equals(userSex, value))
                    return;
                userSex = value;

                OnPropertyChanged();
            }
        }
    }
}
