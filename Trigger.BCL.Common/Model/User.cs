using Trigger.BCL.Common.Datastore;
using Trigger.XStorable.DataStore;
using Trigger.XForms;
using Eto.Drawing;
using System.IO;

namespace Trigger.BCL.Common.Model
{
    public enum Sex
    {
        Female,
        Male
    }

    [System.ComponentModel.DefaultProperty("UserName")]
    public class User : StorableBase , IFileData
    {
        public override void Initialize()
        {
            //INFO: Do not initialize!!! 
        }

        bool allowAdministration;

        public bool AllowAdministration
        {
            get
            {
                return allowAdministration;
            }
            set
            {
                if (Equals(allowAdministration, value))
                    return;
                allowAdministration = value;

                OnPropertyChanged();
            }
        }

        string subject;

        public string Subject
        {
            get
            {
                return subject;
            }
            set
            {
                if (Equals(subject, value))
                    return;
                subject = value;

                OnPropertyChanged();
            }
        }

        string fileName;

        [FieldImageData(true)]
        public string FileName
        {
            get
            {
                return fileName;
            }
            set
            {
                if (Equals(fileName, value))
                    return;
                fileName = value;

                OnPropertyChanged();
            }
        }

        Image avatar;

        [System.Runtime.Serialization.IgnoreDataMember]
        public Image Avatar
        {
            get
            {
                var file = FileName.GetValidPath();
                if (File.Exists(file))
                {
                    if (avatar == null)
                        avatar = new Bitmap(FileName.GetValidPath());
                    return avatar;
                }
                return null;
            }
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
