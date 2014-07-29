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
    [ViewDescriptor(typeof(AreaViewDescriptor))]
    public class Area : StorableBase
    {
        [System.ComponentModel.DisplayName("Area")]
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
                //sb.AppendLine(string.Format("ID: {0}", MappingId));
                return sb.ToString();
            }
        }

        string name;

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
    }
}
