using System;
using Eto.Forms;
using Trigger.Datastore.Persistent;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;

namespace Trigger.WinForms.Layout
{
	public class DetailViewGenerator
	{
		protected DetailPropertyEditorFactory EditorFactory
		{
			get;
			set;
		}

		protected IStorable Model
		{
			get;
			set;
		}

		public DetailViewGenerator(IStorable model)
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
					if (property.Name.EndsWith("Alias"))
						continue;
	
					var visibilityAttribute = property.GetCustomAttributes(typeof(VisibleOnViewAttribute), true).FirstOrDefault() as VisibleOnViewAttribute;

					if (visibilityAttribute != null && (visibilityAttribute.TargetView == TargetView.ListOnly || visibilityAttribute.TargetView == TargetView.None))
						continue;

					if (property.PropertyType.IsGenericType)
					{
						var value = property.GetValue(Model, null);
						if (value is IEnumerable<IStorable>)
						{
							var firstItem = (value as IEnumerable<IStorable>).FirstOrDefault();

							if (firstItem != null)
							{
								/*
								AddLabel(layout, property);
					
								var gridView = new ListViewGenerator(firstItem.GetType()).GetContent();
								gridView.DataStore = new DataStoreCollection(value as IEnumerable<IPersistent>);
								gridView.Size = new Eto.Drawing.Size(-1, 120);
								layout.Add(gridView, true, false);
								layout.EndHorizontal();
								*/
							}
						}

					}

					if (property.PropertyType == typeof(string))
					{
						AddLabel(layout, property);
						layout.Add(EditorFactory.StringPropertyEditor(property), true);
						layout.EndHorizontal();
					}

					if (property.PropertyType == typeof(DateTime?) || property.PropertyType == typeof(DateTime))
					{
						AddLabel(layout, property);
						layout.Add(EditorFactory.DateTimePropertyEditor(property), true);
						layout.EndHorizontal();
					}

					if (property.PropertyType == typeof(TimeSpan?) || property.PropertyType == typeof(TimeSpan))
					{
						AddLabel(layout, property);
						layout.Add(EditorFactory.TimeSpanPropertyEditor(property), true);
						layout.EndHorizontal();
					}

					if (property.PropertyType == typeof(bool))
					{
						AddLabel(layout, property);
						layout.Add(EditorFactory.BooleanPropertyEditor(property), true);
						layout.EndHorizontal();
					}

					if (typeof(IStorable).IsAssignableFrom(property.PropertyType))
					{
						AddLabel(layout, property);
						var referenceComboBox = EditorFactory.ReferencePropertyEditor(property);

						layout.Add(referenceComboBox, true);

						//AddReferenceButtons(layout, referenceComboBox);

						layout.EndHorizontal();
					}

					if (property.PropertyType.BaseType == typeof(Enum))
					{
						AddLabel(layout, property);
						layout.Add(EditorFactory.EnumPropertyEditor(property), true);
						layout.EndHorizontal();
					}
				}
			}

			layout.BeginHorizontal();
			layout.EndHorizontal();
		
			return layout;
		}

		void AddLabel(DynamicLayout layout, PropertyInfo property)
		{
			var attribute = property.GetCustomAttributes(typeof(System.ComponentModel.DisplayNameAttribute), true).FirstOrDefault() as System.ComponentModel.DisplayNameAttribute;

			layout.BeginVertical();
			var label = new Label
			{
				Text = (attribute != null ? attribute.DisplayName : property.Name) + ":"
			};

			label.Font = new Eto.Drawing.Font(label.Font.Family, 8f);
			layout.Add(label, true);
			layout.EndVertical();
			layout.BeginHorizontal();
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