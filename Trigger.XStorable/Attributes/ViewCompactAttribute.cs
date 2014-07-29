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

    [System.ComponentModel.Category("XForms")]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = false, Inherited = true)]
    public sealed class ViewDescriptorAttribute : Attribute
    {

        public Type DescriptorType
        {
            get;
            private set;
        }

        public ViewDescriptorAttribute(Type descriptorType)
        {
            this.DescriptorType = descriptorType;

        }
    }
}
