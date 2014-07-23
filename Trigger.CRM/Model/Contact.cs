using Trigger.Datastore.Persistent;

namespace Trigger.CRM.Model
{

	[System.ComponentModel.DefaultProperty("PhoneNumber")]
	public class Contact : StorableBase
	{
		public override string GetRepresentation()
		{
			var sb = new System.Text.StringBuilder();
			sb.AppendLine(string.Format("{0} - {1} / {2}", PersonAlias, PhoneNumber, Email));
			return sb.ToString();
		}

		[System.ComponentModel.DisplayName("Person")]
		[VisibleOnView(TargetView.ListOnly)]
		public string PersonAlias
		{
			get
			{
				return Person != null ? Person.FirstName + " " + Person.LastName : null;
			}
		}

		Person person;

		[System.ComponentModel.DisplayName("Person")]
		[LinkedObject]
		[VisibleOnView(TargetView.DetailOnly)]
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

		[System.ComponentModel.DisplayName("Contact")]
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

		[System.ComponentModel.DisplayName("Phone")]
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

		[System.ComponentModel.DisplayName("Mobile")]
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

		[System.ComponentModel.DisplayName("E-Mail")]
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
