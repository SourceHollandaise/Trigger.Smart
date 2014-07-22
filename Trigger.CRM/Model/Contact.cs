using Trigger.Datastore.Persistent;

namespace Trigger.CRM.Model
{

	[System.ComponentModel.DefaultProperty("PhoneNumber")]
	public class Contact : PersistentModelBase
	{
		public override string GetRepresentation()
		{
			var sb = new System.Text.StringBuilder();
			sb.AppendLine(string.Format("{0} - {1} / {2}", PersonAlias, PhoneNumber, Email));
			return sb.ToString();
		}

		public string PersonAlias
		{
			get
			{
				return Person != null ? Person.FirstName + " " + Person.LastName : null;
			}
		}

		Person person;

		[PersistentReference("PersonAlias")]
		public Person Person
		{
			get
			{
				return person;
			}
			set
			{
				if (Equals(person, value))
					return;
				person = value;

				OnPropertyChanged();
			}
		}

		ContactType contactType;

		public ContactType ContactType
		{
			get
			{
				return contactType;
			}
			set
			{
				if (Equals(contactType, value))
					return;
				contactType = value;

				OnPropertyChanged();
			}
		}

		string phoneNumber;

		public string PhoneNumber
		{
			get
			{
				return phoneNumber;
			}
			set
			{
				if (Equals(phoneNumber, value))
					return;
				phoneNumber = value;

				OnPropertyChanged();
			}
		}

		string mobileNumber;

		public string MobileNumber
		{
			get
			{
				return mobileNumber;
			}
			set
			{
				if (Equals(mobileNumber, value))
					return;
				mobileNumber = value;

				OnPropertyChanged();
			}
		}

		string email;

		public string Email
		{
			get
			{
				return email;
			}
			set
			{
				if (Equals(email, value))
					return;
				email = value;

				OnPropertyChanged();
			}
		}
	}
}
