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
	public class LinkedListAttribute : Attribute
	{
		public Type LinkType
		{
			get;
			private set;
		}

		public LinkedListAttribute(Type linkType)
		{
			LinkType = linkType;
		}
	}
}
