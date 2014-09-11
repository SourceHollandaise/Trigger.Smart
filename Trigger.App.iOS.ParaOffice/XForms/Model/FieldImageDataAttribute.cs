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


        public int DefaultWidth
        {
            get;
            private set;
        }


        public int DefaultHeight
        {
            get;
            private set;
        }

        public FieldImageDataAttribute(bool thumbnail = false, int defaultWidth = 128, int defaultHeight = 128)
        {
            this.DefaultHeight = defaultHeight;
            this.DefaultWidth = defaultWidth;
            this.Thumbnail = thumbnail;
            
        }
    }
}
