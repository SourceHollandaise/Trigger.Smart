using System;

namespace XForms.Model
{

    [System.ComponentModel.Category("XForms")]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = true, AllowMultiple = false)]
    public sealed class ImageNameAttribute : Attribute
    {
        public string ImageName
        {
            get;
            set;
        }

        public ImageNameAttribute(string imageName)
        {
            this.ImageName = imageName;
            
        }
    }
}
