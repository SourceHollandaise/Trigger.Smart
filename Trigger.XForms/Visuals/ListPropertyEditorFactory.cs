using System;
using Eto.Forms;
using Trigger.XStorable.DataStore;
using System.Reflection;
using System.Linq;

namespace Trigger.XForms.Visuals
{
    public class ListPropertyEditorFactory
    {
        protected Type ModelType
        {
            get;
            set;
        }

        public ListPropertyEditorFactory(Type modelType)
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

            return null;
        }
    }
}