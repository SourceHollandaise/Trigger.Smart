using Trigger.XStorable.DataStore;
using System.IO;
using Trigger.XStorable.Model;
using Trigger.BCL.Common.Model;

namespace Trigger.BCL.EventTracker.Model
{
    [System.ComponentModel.DefaultProperty("Subject")]
    [CompactViewRepresentation]
    [MainViewItem]
    public class Document : StorableBase, IFileData
    {
        [System.ComponentModel.DisplayName("Document")]
        [VisibleOnView(TargetView.None)]
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
            var path = Path.Combine(StoreConfigurator.DocumentStoreLocation, FileName);

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

        string fileName;

        [VisibleOnView(TargetView.DetailOnly)]
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

        string description;

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

        [System.ComponentModel.DisplayName("Area")]
        [System.Runtime.Serialization.IgnoreDataMember]
        [VisibleOnView(TargetView.ListOnly)]
        public string AreaAlias
        {
            get
            {
                return Area != null ? Area.Name : null;
            }
        }

        Area area;

        [LinkedObject]
        [VisibleOnView(TargetView.DetailOnly)]
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
        [VisibleOnView(TargetView.ListOnly)]
        public string IssueAlias
        {
            get
            {
                return Issue != null ? Issue.Subject : null;
            }
        }

        IssueTracker issue;

        [LinkedObject]
        [VisibleOnView(TargetView.DetailOnly)]
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
        [VisibleOnView(TargetView.ListOnly)]
        public string UserAlias
        {
            get
            {
                return User != null ? User.UserName : null;
            }
        }

        User user;

        [System.ComponentModel.DisplayName("From user")]
        [LinkedObject]
        [VisibleOnView(TargetView.DetailOnly)]
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
    }
}
