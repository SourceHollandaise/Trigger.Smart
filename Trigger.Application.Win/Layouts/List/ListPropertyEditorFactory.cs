using System;
using Eto.Forms;
using Trigger.Datastore.Persistent;
using System.Reflection;
using System.Linq;
using Eto;

namespace Trigger.WinForms.Layout
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
			if (typeof(IPersistent).IsAssignableFrom(property.PropertyType.BaseType))
			{
				var attribute = property.GetCustomAttributes(typeof(PersistentReferenceAttribute), true).FirstOrDefault() as PersistentReferenceAttribute;
				if (attribute != null && !string.IsNullOrWhiteSpace(attribute.AliasProperty))
					return new TextBoxCell(attribute.AliasProperty);
			}

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
				if (!property.Name.EndsWith("Alias"))
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