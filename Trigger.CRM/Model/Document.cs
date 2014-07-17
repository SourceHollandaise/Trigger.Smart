using System;

namespace Trigger.CRM.Model
{
    public class Document : ModelBase
    {
        public override string GetRepresentation()
        {
            var sb = new System.Text.StringBuilder();
            sb.AppendLine(string.Format("'{0}' by {1} on {2}", Subject, User != null ? User.UserName : "anonymous", Created));
            sb.AppendLine(string.Format("Linked to '{0}' project", Project != null ? Project.Name : "anonymous"));
            sb.AppendLine(string.Format("{0}", Description));
            sb.AppendLine(string.Format("ID: {0}", MappingId));
            return sb.ToString();
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

        DateTime? created;

        public DateTime? Created
        {
            get
            {
                return created;
            }
            set
            {
                if (Equals(created, value))
                    return;
                created = value;

                OnPropertyChanged();
            }
        }

        Project project;

        public Project Project
        {
            get
            {
                return project;
            }
            set
            {
                if (Equals(project, value))
                    return;
                project = value;

                OnPropertyChanged();
            }
        }

        IssueTracker issue;

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

        User user;

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

        string fileName;

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
