using System;

namespace Trigger.XStorable.DataStore
{

	[System.ComponentModel.Category("Store")]
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class LinkedListAttribute : Attribute
	{
		public Type LinkType
		{
			get;
			private set;
		}

		public LinkedListAttribute(Type linkType)
		{
			this.LinkType = linkType;
		}
	}
}
