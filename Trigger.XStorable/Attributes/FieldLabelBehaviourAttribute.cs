using System;

namespace Trigger.XStorable.DataStore
{

    [System.ComponentModel.Category("XForms")]
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public sealed class FieldLabelBehaviourAttribute : Attribute
    {

        public string PlaceholderText
        {
            get;
            private set;
        }

        public bool ShowLabel
        {
            get;
            private set;
        }

        public FieldLabelBehaviourAttribute(bool showLabel = true, string placeholderText = null)
        {
            this.ShowLabel = showLabel;
            this.PlaceholderText = placeholderText;
            
        }
    }
}
