
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
			sb.AppendLine(string.Format("'{0}' by {1} started at {2}", Subject, UserAlias, Begin));
			if (Duration != null)
				sb.AppendLine(string.Format("{0} - {1} with duration {2}", Begin, End, Duration));
			sb.AppendLine(string.Format("Linked to '{0}' area", AreaAlias));
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

		[System.Runtime.Serialization.IgnoreDataMember]
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

		[System.Runtime.Serialization.IgnoreDataMember]
		public string UserAlias
		{
			get
			{
				return User != null ? User.UserName : null;
			}
		}

		User user;

		[PersistentReference("UserAlias")]
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

