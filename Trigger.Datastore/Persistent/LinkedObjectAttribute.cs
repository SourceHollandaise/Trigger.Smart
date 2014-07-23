using System;

namespace Trigger.Datastore.Persistent
{
	public enum TargetView
	{
		Any,
		DetailOnly,
		ListOnly,
		None
	}

	[System.ComponentModel.Category("Store")]
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class LinkedObjectAttribute : Attribute
	{

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

	[System.ComponentModel.Category("XForms")]
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

	[System.ComponentModel.Category("XForms")]
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = false, Inherited = true)]
	public sealed class CompactViewRepresentationAttribute : Attribute
	{
		public string VisualProperty
		{
			get;
			set;
		}

		public CompactViewRepresentationAttribute(string visualProperty = "GetRepresentation")
		{
			this.VisualProperty = visualProperty;
		}
	}
}
