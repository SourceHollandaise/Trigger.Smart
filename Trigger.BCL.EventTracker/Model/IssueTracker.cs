
using System;
using Trigger.XStorable.DataStore;
using Trigger.BCL.Common.Model;
using Trigger.BCL.Common.Security;
using Trigger.XForms;
using Trigger.BCL.Common.Datastore;

namespace Trigger.BCL.EventTracker.Model
{
    [System.ComponentModel.DefaultProperty("Subject")]
    [System.ComponentModel.DisplayName("Issue")]
    [ImageName("note_edit")]
    public class IssueTracker : StorableBase, IFileData
    {
        public override void Initialize()
        {
            base.Initialize();

            IssuePriority = Priority.High;
            IssueType = IssueType.Request;
            IssueState = IssueState.Open;
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

        Priority issuePriority;

        [System.ComponentModel.DisplayName("Priority")]
        public Priority IssuePriority
        {
            get
            {
                return issuePriority;
            }
            set
            {
                if (Equals(issuePriority, value))
                    return;
                issuePriority = value;

                OnPropertyChanged();
            }
        }

        IssueType issueType;

        [System.ComponentModel.DisplayName("Type")]
        public IssueType IssueType
        {
            get
            {
                return issueType;
            }
            set
            {
                if (Equals(issueType, value))
                    return;
                issueType = value;

                OnPropertyChanged();
            }
        }

        IssueState issueState;

        [System.ComponentModel.DisplayName("State")]
        public IssueState IssueState
        {
            get
            {
                return issueState;
            }
            set
            {
                if (Equals(issueState, value))
                    return;
                issueState = value;

                OnPropertyChanged();

                UpdateIssue();
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

        DateTime? start;

        public DateTime? Start
        {
            get
            {
                return start;
            }
            set
            {
                if (Equals(start, value))
                    return;
                start = value;

                OnPropertyChanged();
            }
        }

        public string StartedByAlias
        {
            get
            {
                return StartedBy != null ? StartedBy.UserName : null;
            }
        }

        User startedBy;

        [System.ComponentModel.DisplayName("Started by")]
        [LinkedObject]
        public User StartedBy
        {
            get
            {
                return startedBy;
            }
            set
            {
                if (Equals(startedBy, value))
                    return;
                startedBy = value;

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

        public string ResolvedByAlias
        {
            get
            {
                return ResolvedBy != null ? ResolvedBy.UserName : null;
            }
        }

        User resolvedBy;

        [System.ComponentModel.DisplayName("Resolved by")]
        [LinkedObject]
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

        bool isDone;

        [System.ComponentModel.DisplayName("Done")]
        [System.ComponentModel.ReadOnly(true)]
        public bool IsDone
        {
            get
            {
                return isDone;
            }
            protected set
            {
                if (Equals(IsDone, value))
                    return;
                isDone = value;

                OnPropertyChanged();
            }
        }

        string duration;

        [System.ComponentModel.ReadOnly(true)]
        public string Duration
        {
            get
            {
                return duration;
            }
            protected set
            {
                if (Equals(duration, value))
                    return;
                duration = value;

                OnPropertyChanged();
            }
        }

        string fileName;

        [System.ComponentModel.ReadOnly(true)]
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

        void UpdateIssue()
        {
            switch (IssueState)
            {
                case IssueState.Open:
                case IssueState.Accepted:
                    ResolvedBy = null;
                    Resolved = null;
                    StartedBy = null;
                    Start = null;
                    Duration = null;
                    IsDone = false;
                    break;
                case IssueState.InProgress:
                    if (!Start.HasValue)
                        Start = DateTime.Now;
                    StartedBy = Map.ResolveInstance<ISecurityInfoProvider>().CurrentUser;
                    break;
                case IssueState.Done:
                case IssueState.Rejected:
                    ResolvedBy = Map.ResolveInstance<ISecurityInfoProvider>().CurrentUser;
                    Resolved = DateTime.Now;
                    IsDone = true;
                    Duration = (Resolved - Start).ToString();
                    break;
            }
        }
    }
}
