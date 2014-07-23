using System;

namespace Trigger.Datastore.Persistent
{
	[System.ComponentModel.Category("Store")]
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class LinkedObjectAttribute : Attribute
	{
		//		public string AliasProperty
		//		{
		//			get;
		//			private set;
		//		}
		//
		//		public LinkedObjectAttribute(string aliasProperty)
		//		{
		//			this.AliasProperty = aliasProperty;
		//		}
	}

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

	[System.ComponentModel.Category("View")]
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class VisibleOnViewAttribute : Attribute
	{

		public TargetView TargetView
		{
			get;
			private set;
		}

		public VisibleOnViewAttribute(TargetView targetView)
		{
			this.TargetView = targetView;
			
		}
	}

	public enum TargetView
	{
		Any,
		DetailOnly,
		ListOnly,
		None
	}
}
