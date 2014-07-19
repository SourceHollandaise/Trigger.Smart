using Trigger.Datastore.Persistent;

namespace Trigger.Datastore.Security
{
	[System.ComponentModel.DefaultProperty("UserName")]
	public class User : PersistentModelBase
	{
		public override void Initialize()
		{
			//INFO: Do not initialize!!! 
		}

		public override string GetRepresentation()
		{
			var sb = new System.Text.StringBuilder();
			sb.AppendLine(string.Format("{0}", UserName));
			sb.AppendLine(string.Format("{0}", EMail));
			sb.AppendLine(string.Format("ID: {0}", MappingId));
			return sb.ToString();
		}

		string userName;

		public string UserName
		{
			get
			{
				return userName;
			}
			set
			{
				if (Equals(userName, value))
					return;
				userName = value;

				OnPropertyChanged();
			}
		}

		string password;

		public string Password
		{
			get
			{
				return password;
			}
			private set
			{
				if (Equals(password, value))
					return;
				password = value;
    
				OnPropertyChanged();
			}
		}

		string eMail;

		public string EMail
		{
			get
			{
				return eMail;
			}
			set
			{
				if (Equals(eMail, value))
					return;
				eMail = value;

				OnPropertyChanged();
			}
		}

		public void SetPassword(string input)
		{       
			Password = SecureText.Secure(input);
		}
	}
}
