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
			sb.AppendLine(string.Format("'{0}' by {1} on {2}", Subject, User != null ? User.UserName : "anonymous", Created));
			sb.AppendLine(string.Format("Linked to '{0}' project", Area != null ? Area.Name : "anonymous"));
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

		Area project;

		[PersistentReference]
		public Area Area
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

		IssueTracker issue;

		[PersistentReference]
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
	}
}
