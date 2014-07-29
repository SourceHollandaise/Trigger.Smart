using System;

namespace Trigger.XStorable.DataStore
{

    [System.ComponentModel.Category("XForms")]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = false, Inherited = true)]
    public sealed class ViewCompactAttribute : Attribute
    {
        public string VisualProperty
        {
            get;
            private set;
        }

        public ViewCompactAttribute(string visualProperty = "GetRepresentation")
        {
            this.VisualProperty = visualProperty;
        }
    }
}
