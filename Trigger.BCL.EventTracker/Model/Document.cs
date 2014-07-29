using Trigger.XStorable.DataStore;
using System.IO;
using Trigger.XStorable.Model;
using Trigger.BCL.Common.Model;

namespace Trigger.BCL.EventTracker.Model
{
    [System.ComponentModel.DefaultProperty("Subject")]
    [ViewCompact]
    [ViewNavigation]
    [ViewDescriptor(typeof(DocumentViewDescriptor))]
    public class Document : StorableBase, IFileData
    {
        [System.ComponentModel.DisplayName("Document")]
        [FieldVisible(TargetView.None)]
        public override string GetRepresentation
        {
            get
            {
                var sb = new System.Text.StringBuilder();
                sb.AppendLine(string.Format("'{0}' by {1}", Subject, User.UserName));
                sb.AppendLine(string.Format("Linked to '{0}' area", Area.Name));
                sb.AppendLine(string.Format("{0}", Description));
                //sb.AppendLine(string.Format("ID: {0}", MappingId));
                return sb.ToString();
            }
        }

        public override void Delete()
        {
            var path = Path.Combine(Map.ResolveInstance<IStoreConfiguration>().DocumentStoreLocation, FileName);

            if (File.Exists(path))
                File.Delete(path);

            base.Delete();
        }

        string subject;

        [FieldGroup("Document-Details", 1, 1)]
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

        [FieldGroup("Document-Details", 1, 2)]
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
