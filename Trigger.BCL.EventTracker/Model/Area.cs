using Trigger.XStorable.DataStore;
using System.Collections.Generic;
using System.Linq;
using Trigger.XStorable.Model;
using Trigger.BCL.Common.Model;

namespace Trigger.BCL.EventTracker.Model
{
    [System.ComponentModel.DefaultProperty("Name")]
    [ViewCompact]
    [ViewNavigation]
    public class Area : StorableBase
    {
        [System.ComponentModel.DisplayName("Area")]
        [FieldVisible(TargetView.None)]
        public override string GetRepresentation
        {
            get
            {
                var sb = new System.Text.StringBuilder();
                sb.AppendLine(string.Format("{0}", Name));
                sb.AppendLine(string.Format("{0}", Description));
                if (LinkedDocuments.Any())
                    sb.AppendLine(string.Format("Documents linked: {0}", LinkedDocuments.Count()));
                if (LinkedIssues.Any())
                    sb.AppendLine(string.Format("Issues linked: {0}", LinkedIssues.Count()));
                if (LinkedTrackedTimes.Any())
                    sb.AppendLine(string.Format("Tracked times linked: {0}", LinkedTrackedTimes.Count()));
                //sb.AppendLine(string.Format("ID: {0}", MappingId));
                return sb.ToString();
            }
        }

        string name;

        [FieldGroup("Area-Details", 1, 1)]
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (Equals(name, value))
                    return;
                name = value;

                OnPropertyChanged();
            }
        }

        string description;

        [FieldGroup("Area-Details", 1, 2)]
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

        [FieldGroup("Links", 2, 1)]
        [System.ComponentModel.DisplayName("Linked documents")]
        [System.Runtime.Serialization.IgnoreDataMember]
        [LinkedList(typeof(Document))]
        public IEnumerable<Document> LinkedDocuments
        {
            get
            {
                return Store.LoadAll<Document>().Where(p => p.Area != null && p.Area.MappingId.Equals(MappingId));
            }
        }

        [FieldGroup("Links", 2, 2)]
        [System.ComponentModel.DisplayName("Linked issues")]
        [System.Runtime.Serialization.IgnoreDataMember]
        [LinkedList(typeof(IssueTracker))]
        public IEnumerable<IssueTracker> LinkedIssues
        {
            get
            {
                return Store.LoadAll<IssueTracker>().Where(p => p.Area != null && p.Area.MappingId.Equals(MappingId));
            }
        }

        [FieldGroup("Links", 2, 3)]
        [System.ComponentModel.DisplayName("Linked tracked times")]
        [System.Runtime.Serialization.IgnoreDataMember]
        [LinkedList(typeof(TimeTracker))]
        public IEnumerable<TimeTracker> LinkedTrackedTimes
        {
            get
            {
                return Store.LoadAll<TimeTracker>().Where(p => p.Area != null && p.Area.MappingId.Equals(MappingId));
            }
        }
    }
}
