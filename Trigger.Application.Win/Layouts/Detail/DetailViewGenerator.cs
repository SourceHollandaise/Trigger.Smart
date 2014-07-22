using System;
using System.Collections.Generic;
using Eto.Forms;
using Trigger.Datastore.Persistent;
using System.Reflection;
using System.Linq;

namespace Trigger.WinForms.Layout
{
	public class DetailViewGenerator
	{
		protected DetailPropertyEditorFactory EditorFactory
		{
			get;
			set;
		}

		protected IPersistent Model
		{
			get;
			set;
		}

		public DetailViewGenerator(IPersistent model)
		{
			this.Model = model;
			EditorFactory = new DetailPropertyEditorFactory(Model);
		}

		public DynamicLayout GetContent()
		{
			var layout = new DynamicLayout();

			var properties = Model.GetType().GetProperties();

			foreach (var property in properties)
			{
				if (property.CanRead)
				{
					var attribute = property.GetCustomAttributes(typeof(System.ComponentModel.DisplayNameAttribute), true).FirstOrDefault() as System.ComponentModel.DisplayNameAttribute;

					if (property.Name.EndsWith("Alias"))
						continue;

					if (property.PropertyType.IsGenericType)
					{
						continue;
					}
					layout.BeginHorizontal();
					layout.Add(new Label
					{
						Text = attribute != null ? attribute.DisplayName : property.Name
					});

					if (property.PropertyType == typeof(string))
					{
						layout.Add(EditorFactory.StringPropertyEditor(property), true);
						layout.EndHorizontal();
					}

					if (property.PropertyType == typeof(DateTime?) || property.PropertyType == typeof(DateTime))
					{
						layout.Add(EditorFactory.DateTimePropertyEditor(property), true);
						layout.EndHorizontal();
					}

					if (property.PropertyType == typeof(TimeSpan?) || property.PropertyType == typeof(TimeSpan))
					{
						layout.Add(EditorFactory.TimeSpanPropertyEditor(property), true);
						layout.EndHorizontal();
					}

					if (property.PropertyType == typeof(bool))
					{
						layout.Add(EditorFactory.BooleanPropertyEditor(property), true);
						layout.EndHorizontal();
					}

					if (typeof(IPersistent).IsAssignableFrom(property.PropertyType))
					{
						var referenceComboBox = EditorFactory.ReferencePropertyEditor(property);

						layout.Add(referenceComboBox, true);

						AddReferenceButtons(layout, referenceComboBox);

						layout.EndHorizontal();
					}

					if (property.PropertyType.BaseType == typeof(Enum))
					{
						layout.Add(EditorFactory.EnumPropertyEditor(property), true);
						layout.EndHorizontal();
					}
				}
			}

			layout.BeginHorizontal();
			layout.EndHorizontal();
		
			return layout;
		}

		void AddReferenceButtons(DynamicLayout layout, ComboBox referenceComboBox)
		{
			layout.BeginVertical();
			layout.BeginHorizontal();
			layout.Add(EditorFactory.ReferenceOpenButton(referenceComboBox), true);
			layout.Add(EditorFactory.ReferenceClearButton(referenceComboBox), true);
			layout.EndHorizontal();
			layout.EndVertical();
		}
	}
}