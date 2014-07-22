using Trigger.Datastore.Persistent;
using Trigger.Datastore.Security;
using System.IO;
using Trigger.CRM.Persistent;

namespace Trigger.CRM.Model
{
	[System.ComponentModel.DefaultProperty("Subject")]
	public class Document : PersistentModelBase, IFileData
	{
		public override string GetRepresentation()
		{
			var sb = new System.Text.StringBuilder();
			sb.AppendLine(string.Format("'{0}' by {1} on {2}", Subject, UserAlias, Created));
			sb.AppendLine(string.Format("Linked to '{0}' area", AreaAlias));
			sb.AppendLine(string.Format("{0}", Description));
			sb.AppendLine(string.Format("ID: {0}", MappingId));
			return sb.ToString();
		}

		public override void Delete()
		{
			var path = Path.Combine(StoreConfigurator.DocumentStoreLocation, FileName);

			if (File.Exists(path))
				File.Delete(path);

			base.Delete();
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

		string fileName;

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
		public string IssueAlias
		{
			get
			{
				return Issue != null ? Issue.Subject : null;
			}
		}

		IssueTracker issue;

		[PersistentReference("IssueAlias")]
		public IssueTracker Issue
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
	}
}
