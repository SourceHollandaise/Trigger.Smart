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
            private set;
        }

        public CompactViewItemAttribute(string visualProperty = "GetRepresentation")
        {
            this.VisualProperty = visualProperty;
        }
    }

    [System.ComponentModel.Category("Store")]
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class InGroupAttribute : Attribute
    {

        public string GroupName
        {
            get;
            private set;
        }


        public int PropertyIndex
        {
            get;
            set;
        }

        public  int GroupIndex
        {
            get;
            set;
        }

        public InGroupAttribute(string groupName, int groupIndex, int propertyIndex)
        {
            this.GroupIndex = groupIndex;
            this.PropertyIndex = propertyIndex;
            this.GroupName = groupName;
            
        }
    }
}
