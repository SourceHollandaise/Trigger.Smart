using System;

namespace Trigger.XStorable.DataStore
{
    public enum TargetView
    {
        Any,
        DetailOnly,
        ListOnly,
        None
    }

    [System.ComponentModel.Category("XForms")]
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public sealed class VisibleOnViewAttribute : Attribute
    {
        public TargetView TargetView
        {
            get;
            private set;
        }

        public VisibleOnViewAttribute(TargetView targetView = TargetView.Any)
        {
            this.TargetView = targetView;
        }
    }
}
