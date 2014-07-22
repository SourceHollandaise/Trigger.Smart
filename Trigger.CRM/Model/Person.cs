using Trigger.Datastore.Persistent;
using System.Collections.Generic;
using System.Linq;

namespace Trigger.CRM.Model
{
	[System.ComponentModel.DefaultProperty("FirstName")]
	public class Person : PersistentModelBase
	{
		public override string GetRepresentation()
		{
			var sb = new System.Text.StringBuilder();
			sb.AppendLine(string.Format("{0} {1}", FirstName, LastName));
			return sb.ToString();
		}

		string firstName;

		public string FirstName
		{
			get
			{
				return firstName;
			}
			set
			{
				if (Equals(firstName, value))
					return;
				firstName = value;

				OnPropertyChanged();
			}
		}

		string middleName;

		public string MiddleName
		{
			get
			{
				return middleName;
			}
			set
			{
				if (Equals(middleName, value))
					return;
				middleName = value;

				OnPropertyChanged();
			}
		}

		string lastName;

		public string LastName
		{
			get
			{
				return lastName;
			}
			set
			{
				if (Equals(lastName, value))
					return;
				lastName = value;

				OnPropertyChanged();
			}
		}

		string postalCode;

		public string PostalCode
		{
			get
			{
				return postalCode;
			}
			set
			{
				if (Equals(postalCode, value))
					return;
				postalCode = value;

				OnPropertyChanged();
			}
		}

		string city;

		public string City
		{
			get
			{
				return city;
			}
			set
			{
				if (Equals(city, value))
					return;
				city = value;

				OnPropertyChanged();
			}
		}

		string street;

		public string Street
		{
			get
			{
				return street;
			}
			set
			{
				if (Equals(street, value))
					return;
				street = value;

				OnPropertyChanged();
			}
		}

		[System.Runtime.Serialization.IgnoreDataMember]
		[LinkedList(typeof(Contact))]
		public IEnumerable<Contact> LinkedContacts
		{
			get
			{
				return Store.LoadAll<Contact>().Where(p => p.Person != null && p.Person.MappingId.Equals(MappingId));
			}
		}
	}
}
