using System;
using Eto.Forms;
using Trigger.XStorable.DataStore;
using System.Reflection;

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
            if (property.PropertyType.IsGenericType)
            {
                return null;
            }

            if (property.PropertyType == typeof(bool))
            {
                return new CheckBoxCell(property.Name);
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

            if (typeof(IStorable).IsAssignableFrom(property.PropertyType))
            {
                return new TextBoxCell(property.Name);

                /*
                var attribute = property.PropertyType.FindAttribute<DefaultPropertyAttribute>();
                if (attribute != null)
                {
                    var nestedProperty = property.PropertyType.GetProperty(attribute.Name);
                    return new TextBoxCell(property.Name + "." + nestedProperty.Name);
                }
                */
            }

            return null;
        }
    }
}