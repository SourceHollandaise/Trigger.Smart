using Trigger.XStorable.DataStore;
using System.IO;
using Trigger.XStorable.Model;
using Trigger.BCL.Common.Model;
using Trigger.XForms;

namespace Trigger.BCL.EventTracker.Model
{
    [System.ComponentModel.DefaultProperty("Subject")]
    public class Document : StorableBase, IFileData
    {
        public override void Delete()
        {
            var path = Path.Combine(Map.ResolveInstance<IStoreConfiguration>().DocumentStoreLocation, FileName);

            if (File.Exists(path))
                File.Delete(path);

            base.Delete();
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

        string description;

        [FieldTextArea]
        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                if (Equals(description, value))
                    return;
                description = value;

                OnPropertyChanged();
            }
        }

        string fileName;

        User user;

        [System.ComponentModel.DisplayName("From user")]
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

        Area area;

        [LinkedObject]
        public Area Area
        {
            get
            {
                return area;
            }
            set
            {
                if (Equals(area, value))
                    return;
                area = value;

                OnPropertyChanged();
            }
        }

        IssueTracker issue;

        [LinkedObject]
        public IssueTracker Issue
        {
            get
            {
                return issue;
            }
            set
            {
                if (Equals(issue, value))
                    return;
                issue = value;

                OnPropertyChanged();
            }
        }

        [FieldFileData]
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

    }
}
