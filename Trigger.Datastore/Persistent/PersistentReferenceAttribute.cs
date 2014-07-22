using System;

namespace Trigger.Datastore.Persistent
{
	[AttributeUsage(AttributeTargets.Property)]
	public class PersistentReferenceAttribute : Attribute
	{
		public string AliasProperty
		{
			get;
			private set;
		}

		public PersistentReferenceAttribute(string aliasProperty)
		{
			AliasProperty = aliasProperty;
		}
	}

	[AttributeUsage(AttributeTargets.Property)]
	public class PersistentReferenceAliasAttribute : Attribute
	{
		public string ReferencePropertyName
		{
			get;
			private set;
		}

		public PersistentReferenceAliasAttribute(string referencePropertyName)
		{
			ReferencePropertyName = referencePropertyName;
		}
	}
}
