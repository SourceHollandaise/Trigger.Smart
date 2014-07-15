
using System;
using System.ComponentModel;
using Trigger.CRM.Security;

namespace Trigger.CRM.Model
{
    public class IssueTracker : ModelBase
    {
        public void UpdateIssue(IssueState state)
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

                OnPropertyChanged(new PropertyChangedEventArgs("Issue"));
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

                OnPropertyChanged(new PropertyChangedEventArgs("State"));

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

                OnPropertyChanged(new PropertyChangedEventArgs("Subject"));
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

                OnPropertyChanged(new PropertyChangedEventArgs("Description"));
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

                OnPropertyChanged(new PropertyChangedEventArgs("Project"));
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

                OnPropertyChanged(new PropertyChangedEventArgs("Created"));
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

                OnPropertyChanged(new PropertyChangedEventArgs("Resolved"));
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

                OnPropertyChanged(new PropertyChangedEventArgs("CreatedBy"));
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

                OnPropertyChanged(new PropertyChangedEventArgs("ResolvedBy"));
            }
        }
    }
}
