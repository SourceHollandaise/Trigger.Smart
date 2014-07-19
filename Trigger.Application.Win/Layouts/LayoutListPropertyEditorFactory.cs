using System;
using Eto.Forms;
using Trigger.Datastore.Persistent;
using System.Reflection;
using System.Linq;

namespace Trigger.WinForms.Layout
{
	public class LayoutListPropertyEditorFactory
	{
		protected Type ModelType
		{
			get;
			set;
		}

		public LayoutListPropertyEditorFactory(Type modelType)
		{
			this.ModelType = modelType;
		}

		public Cell CreateDataCell(PropertyInfo property)
		{
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

			if (property.PropertyType == typeof(bool))
			{
				return new CheckBoxCell(property.Name);
			}

			if (property.PropertyType.BaseType is IPersistentId)
			{
				return new TextBoxCell(property.Name);
			}

			if (property.PropertyType.BaseType == typeof(Enum))
			{
				return new TextBoxCell(property.Name);
			}

			return new TextBoxCell(property.Name);
		}
	}
	
}