
using System;
using Trigger.Datastore.Security;
using Trigger.Datastore.Persistent;

namespace Trigger.CRM.Model
{
	[System.ComponentModel.DefaultProperty("Subject")]
	public class IssueTracker : PersistentModelBase
	{
		public override void Initialize()
		{
			base.Initialize();

			Issue = IssueType.Request;
			State = IssueState.Open;
		}

		public override string GetRepresentation()
		{
			var sb = new System.Text.StringBuilder();
			sb.AppendLine(string.Format("'{0}' by {1} on {2}", Subject, CreatedBy != null ? CreatedBy.UserName : "anonymous", Created));
			sb.AppendLine(string.Format("{0} is {1}", Issue, State));
			sb.AppendLine(string.Format("Linked to '{0}' area", Area != null ? Area.Name : "anonymous"));
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

				UpdateIssue();
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

		[PersistentReference("AreaAlias")]
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

		[PersistentReference("ResolvedByAlias")]
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
			if (State == IssueState.Done || State == IssueState.Rejected)
			{
				ResolvedBy = Map.ResolveInstance<ISecurityInfoProvider>().CurrentUser;
				Resolved = DateTime.Now;
				IsDone = true;
				Duration = (Resolved - Created).ToString();
			}
			else
			{
				ResolvedBy = null;
				Resolved = null;
				IsDone = false;
				Duration = null;
			}
		}
	}
}
