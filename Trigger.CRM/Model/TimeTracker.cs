
using System;
using Trigger.Datastore.Persistent;
using Trigger.Datastore.Security;

namespace Trigger.CRM.Model
{
	[System.ComponentModel.DefaultProperty("Subject")]
	public class TimeTracker : PersistentModelBase
	{
		public override string GetRepresentation()
		{
			var sb = new System.Text.StringBuilder();
			sb.AppendLine(string.Format("'{0}' by {1} started at {2}", Subject, User != null ? User.UserName : "anonymous", Begin));
			if (Duration != null)
				sb.AppendLine(string.Format("{0} - {1} with duration {2}", Begin, End, Duration));
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

		DateTime? begin;

		public DateTime? Begin
		{
			get
			{
				return begin;
			}
			set
			{
				if (Equals(begin, value))
					return;
				begin = value;

				OnPropertyChanged();
			}
		}

		DateTime? end;

		public DateTime? End
		{
			get
			{
				return end;
			}
			set
			{
				if (Equals(end, value))
					return;
				end = value;

				OnPropertyChanged();

				UpdateTracker();
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

		Project project;

		[PersistentReference]
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

		User user;

		[PersistentReference]
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

		void UpdateTracker()
		{
			if (End.HasValue)
			{
				IsDone = true;
				Duration = (End - Begin).ToString();
			}
			else
			{
				IsDone = false;
				Duration = null;
			}
		}
	}
}

