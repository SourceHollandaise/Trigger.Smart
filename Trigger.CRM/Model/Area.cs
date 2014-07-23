using Trigger.Datastore.Persistent;
using System.Collections.Generic;
using System.Linq;

namespace Trigger.CRM.Model
{
	[System.ComponentModel.DefaultProperty("Name")]
	public class Area : StorableBase
	{
		public override string GetRepresentation()
		{
			var sb = new System.Text.StringBuilder();
			sb.AppendLine(string.Format("{0}", Name));
			sb.AppendLine(string.Format("{0}", Description));
			sb.AppendLine(string.Format("Documents linked: {0}", LinkedDocuments.Count()));
			sb.AppendLine(string.Format("Issues linked: {0}", LinkedIssues.Count()));
			sb.AppendLine(string.Format("Tracked times linked: {0}", LinkedTrackedTimes.Count()));
			sb.AppendLine(string.Format("ID: {0}", MappingId));
			return sb.ToString();
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
