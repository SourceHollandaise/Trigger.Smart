
using System;
using Trigger.CRM.Security;

namespace Trigger.CRM.Model
{
    public class IssueTracker : ModelBase
    {
        public override string GetRepresentation()
        {
            var sb = new System.Text.StringBuilder();
            sb.AppendLine(string.Format("'{0}' by {1} on {2}", Subject, CreatedBy != null ? CreatedBy.UserName : "anonymous", Created));
            sb.AppendLine(string.Format("{0} is {1}", Issue, State));
            sb.AppendLine(string.Format("Linked to '{0}' project", Project != null ? Project.Name : "anonymous"));
            sb.AppendLine(string.Format("{0}", Description));
            sb.AppendLine(string.Format("ID: {0}", MappingId));
            return sb.ToString();
        }

        IssueType issue;

        public IssueType Issue
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

        IssueState state;

        public IssueState State
        {
            get
            {
                return state;
            }
            set
            {
                if (Equals(state, value))
                    return;
                state = value;

                OnPropertyChanged();

                UpdateIssue(state);
            }
        }

        public bool IsDone
        {
            get
            {
                return Resolved.HasValue && ResolvedBy != null && State == IssueState.Done;
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

        DateTime? resolved;

        public DateTime? Resolved
        {
            get
            {
                return resolved;
            }
            set
            {
                if (Equals(resolved, value))
                    return;
                resolved = value;

                OnPropertyChanged();
            }
        }

        public TimeSpan? Duration
        {
            get
            {
                return Resolved.HasValue && Created.HasValue ? Resolved - Created : null;
            }
        }

        User createdBy;

        public User CreatedBy
        {
            get
            {
                return createdBy;
            }
            set
            {
                if (Equals(createdBy, value))
                    return;
                createdBy = value;

                OnPropertyChanged();
            }
        }

        User resolvedBy;

        public User ResolvedBy
        {
            get
            {
                return resolvedBy;
            }
            set
            {
                if (Equals(resolvedBy, value))
                    return;
                resolvedBy = value;

                OnPropertyChanged();
            }
        }

        void UpdateIssue(IssueState state)
        {
            if (state == IssueState.Done)
            {
                ResolvedBy = Map.ResolveInstance<ISecurityInfoProvider>().CurrentUser;
                Resolved = DateTime.Now;
            }
            else
            {
                ResolvedBy = null;
                Resolved = null;
            }
        }
    }
}
