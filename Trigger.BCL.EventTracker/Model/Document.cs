using Trigger.XStorable.DataStore;
using System.IO;
using Trigger.XStorable.Model;
using Trigger.BCL.Common.Model;

namespace Trigger.BCL.EventTracker.Model
{
    [System.ComponentModel.DefaultProperty("Subject")]
    [ViewCompact]
    [ViewNavigation]
    public class Document : StorableBase, IFileData
    {
        [System.ComponentModel.DisplayName("Document")]
        [FieldVisible(TargetView.None)]
        public override string GetRepresentation
        {
            get
            {
                var sb = new System.Text.StringBuilder();
                sb.AppendLine(string.Format("'{0}' by {1}", Subject, UserAlias));
                sb.AppendLine(string.Format("Linked to '{0}' area", AreaAlias));
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

        [FieldGroup("Document-Details", 1, 4)]
        [System.ComponentModel.DisplayName("From user")]
        [LinkedObject]
        [FieldVisible(TargetView.DetailOnly)]
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


        [System.ComponentModel.DisplayName("Area")]
        [System.Runtime.Serialization.IgnoreDataMember]
        [FieldVisible(TargetView.ListOnly)]
        public string AreaAlias
        {
            get
            {
                return Area != null ? Area.Name : null;
            }
        }

        Area area;

        [FieldGroup("Links", 2, 1)]
        [LinkedObject]
        [FieldVisible(TargetView.DetailOnly)]
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

        [System.ComponentModel.DisplayName("Issue")]
        [System.Runtime.Serialization.IgnoreDataMember]
        [FieldVisible(TargetView.ListOnly)]
        public string IssueAlias
        {
            get
            {
                return Issue != null ? Issue.Subject : null;
            }
        }

        IssueTracker issue;

        [FieldGroup("Links", 2, 2)]
        [LinkedObject]
        [FieldVisible(TargetView.DetailOnly)]
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

        [System.ComponentModel.DisplayName("From user")]
        [System.Runtime.Serialization.IgnoreDataMember]
        [FieldVisible(TargetView.ListOnly)]
        public string UserAlias
        {
            get
            {
                return User != null ? User.UserName : null;
            }
        }

        [FieldGroup("Preview-Details", 3, 1)]
        [FieldVisible(TargetView.DetailOnly)]
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
