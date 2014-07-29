
using System;
using Trigger.XStorable.DataStore;
using Trigger.XStorable.Model;
using Trigger.BCL.Common.Model;
using Trigger.BCL.Common.Security;

namespace Trigger.BCL.EventTracker.Model
{
    [System.ComponentModel.DefaultProperty("Subject")]
    [CompactViewItem]
    [MainViewItem]
    public class IssueTracker : StorableBase, IFileData
    {
        public override void Initialize()
        {
            base.Initialize();

            IssuePriority = Priority.High;
            IssueType = IssueType.Request;
            IssueState = IssueState.Open;
        }

        [System.ComponentModel.DisplayName("Issue")]
        [VisibleOnView(TargetView.None)]
        public override string GetRepresentation
        {
            get
            {
                var sb = new System.Text.StringBuilder();
                sb.AppendLine(string.Format("'{0}' / {1} - {2}", Subject, IssueType, IssueState));
                sb.AppendLine(string.Format("Priority: {0}", IssuePriority));
                sb.AppendLine(string.Format("Linked to '{0}' area", AreaAlias));
                //sb.AppendLine(string.Format("{0}", Description));
                if (IsDone)
                    sb.AppendLine(string.Format("Resolved by {0} / {1}", ResolvedByAlias, Resolved));
                else
                    sb.AppendLine(string.Format("InProgress since {0}", Start));
                //sb.AppendLine(string.Format("ID: {0}", MappingId));
                return sb.ToString();
            }
        }

        string subject;

        [InGroup("Issue-Details", 1, 1)]
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

        [InGroup("Issue-Details", 1, 2)]
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

        [InGroup("Issue-Details", 1, 3)]
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

        [InGroup("Issue-Details", 1, 4)]
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

        [InGroup("Further Informations", 2, 1)]
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

        Area area;

        [LinkedObject]
        [VisibleOnView(TargetView.DetailOnly)]
        [InGroup("Further Informations", 2, 2)]
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



        DateTime? start;

        [InGroup("Start and resolved", 3, 1)]
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

        DateTime? resolved;

        [InGroup("Start and resolved", 3, 2)]
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

        [System.ComponentModel.DisplayName("Resolved by")]
        [System.Runtime.Serialization.IgnoreDataMember]
        [VisibleOnView(TargetView.ListOnly)]
        public string ResolvedByAlias
        {
            get
            {
                return ResolvedBy != null ? ResolvedBy.UserName : null;
            }
        }

        User resolvedBy;

        [InGroup("Start and resolved", 3, 3)]
        [System.ComponentModel.DisplayName("Resolved by")]
        [LinkedObject]
        [VisibleOnView(TargetView.DetailOnly)]
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

        [InGroup("Start and resolved", 3, 4)]
        [System.ComponentModel.DisplayName("Done")]
        [System.ComponentModel.ReadOnly(true)]
        [VisibleOnView(TargetView.ListOnly)]
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

        [InGroup("Start and resolved", 3, 5)]
        [System.ComponentModel.ReadOnly(true)]
        [VisibleOnView(TargetView.ListOnly)]
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

        [InGroup("Preview", 4, 1)]
        [System.ComponentModel.ReadOnly(true)]
        [FileData]
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
                    Start = null;
                    Duration = null;
                    IsDone = false;
                    break;
                case IssueState.InProgress:
                    if (!Start.HasValue)
                        Start = DateTime.Now;
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
