using System;
using Eto.Forms;
using Trigger.XStorable.DataStore;
using System.Reflection;
using Eto.Drawing;

namespace Trigger.XForms.Visuals
{
    public class ListViewControlFactory
    {
        protected Type ModelType
        {
            get;
            set;
        }

        public ListViewControlFactory(Type modelType)
        {
            this.ModelType = modelType;
        }

        public Cell CreateDataCell(PropertyInfo property)
        {
       
            if (property.PropertyType == typeof(bool))
            {
                return new CheckBoxCell(property.Name);
            }

            if (property.PropertyType == typeof(Image))
            {
                return new ImageViewCell(property.Name);
            }

            if (property.PropertyType == typeof(string))
            {
                return new TextBoxCell(property.Name);
            }

            if (property.PropertyType == typeof(DateTime?) || property.PropertyType == typeof(DateTime))
            {
                return new TextBoxCell(property.Name);
            }

            if (property.PropertyType == typeof(TimeSpan?) || property.PropertyType == typeof(TimeSpan))
            {
                return new TextBoxCell(property.Name);
            }
				
            if (property.PropertyType.BaseType == typeof(Enum))
            {
                return new TextBoxCell(property.Name);
            }

            /*)
            if (typeof(IStorable).IsAssignableFrom(property.PropertyType))
            {
                var defaultAttribute = property.PropertyType.FindAttribute<System.ComponentModel.DefaultPropertyAttribute>();
                if (defaultAttribute != null)
                {
                    return new TextBoxCell(defaultAttribute.Name);
                }
                return new TextBoxCell(property.Name);
            }
            */

            return null;
        }
    }
}