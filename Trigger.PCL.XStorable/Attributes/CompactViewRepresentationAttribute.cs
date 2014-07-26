using System;

namespace Trigger.XStorable.DataStore
{

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
