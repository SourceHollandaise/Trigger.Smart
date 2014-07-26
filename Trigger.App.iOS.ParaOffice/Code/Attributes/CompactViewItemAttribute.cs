using System;

namespace Trigger.XStorable.DataStore
{

    [System.ComponentModel.Category("XForms")]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = false, Inherited = true)]
    public sealed class CompactViewItemAttribute : Attribute
    {
        public string VisualProperty
        {
            get;
            set;
        }

        public CompactViewItemAttribute(string visualProperty = "GetRepresentation")
        {
            this.VisualProperty = visualProperty;
        }
    }
}
