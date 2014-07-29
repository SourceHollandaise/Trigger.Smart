using System;

namespace Trigger.XStorable.DataStore
{

    [System.ComponentModel.Category("XForms")]
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class FieldGroupAttribute : Attribute
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

        public FieldGroupAttribute(string groupName, int groupIndex, int propertyIndex)
        {
            this.GroupIndex = groupIndex;
            this.PropertyIndex = propertyIndex;
            this.GroupName = groupName;
            
        }
    }
}
