using System;

namespace XForms.Model
{

    [System.ComponentModel.Category("XForms")]
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public sealed class FieldImageDataAttribute : Attribute
    {

        public bool Thumbnail
        {
            get;
            private set;
        }

        public FieldImageDataAttribute(bool thumbnail = false)
        {
            this.Thumbnail = thumbnail;
            
        }
    }
}
