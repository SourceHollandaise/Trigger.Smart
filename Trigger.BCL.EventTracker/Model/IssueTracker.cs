
using System;
using Trigger.XStorable.DataStore;
using Trigger.XStorable.Model;
using Trigger.BCL.Common.Model;
using Trigger.BCL.Common.Security;

namespace Trigger.BCL.EventTracker.Model
{
    [System.ComponentModel.DefaultProperty("Subject")]
    [CompactViewRepresentation]
    [MainViewItem]
    public class IssueTracker : StorableBase
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
