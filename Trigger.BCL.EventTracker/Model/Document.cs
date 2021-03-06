using System.IO;
using XForms.Model;
using XForms.Store;

namespace Trigger.BCL.EventTracker.Model
{
    [System.ComponentModel.DefaultProperty("Subject")]
    [System.ComponentModel.DisplayName("Document")]
    [ImageName("folder3_document")]
    public class Document : StorableBase, IFileData
    {
        public override string GetRepresentation
        {
            get
            {
                return Subject;
            }
        }

        public override string GetSearchString()
        {
            return Subject + Description;
        }

        public override void Delete()
        {
            if (!string.IsNullOrWhiteSpace(FileName))
            {
                var path = Path.Combine(Map.ResolveInstance<IStoreConfiguration>().DocumentStoreLocation, FileName);

                if (File.Exists(path))
                    File.Delete(path);
            }

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

        public string UserAlias
        {
            get
            {
                return User != null ? User.UserName : null;
            }
        }

        ApplicationUser user;

        [System.ComponentModel.DisplayName("From user")]
        [LinkedObject]
        public ApplicationUser User
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

        public string AreaAlias
        {
            get
            {
                return Area != null ? Area.Name : null;
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

        public string IssueAlias
        {
            get
            {
                return Issue != null ? Issue.Subject : null;
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
