using System;
using Eto.Forms;
using Trigger.Datastore.Persistent;
using System.Reflection;
using System.Collections.Generic;

namespace Trigger.WinForms.Layout
{
	public class DetailViewGenerator
	{
		DetailPropertyEditorFactory editorFactory;

		protected IPersistent Model
		{
			get;
			set;
		}

		public DetailViewGenerator(IPersistent model)
		{
			this.Model = model;
			editorFactory = new DetailPropertyEditorFactory(Model);
		}

		public DynamicLayout GetContent()
		{
			var layout = new DynamicLayout();
			var properties = Model.GetType().GetProperties();

			foreach (var property in properties)
			{
				if (property.CanRead)
				{
					if (property.PropertyType == typeof(string))
					{
						layout.BeginHorizontal();
						layout.Add(new Label
						{
							Text = property.Name
						});
	
						layout.Add(editorFactory.StringPropertyEditor(property), true);
						layout.EndHorizontal();
					}

					if (property.PropertyType == typeof(DateTime?) || property.PropertyType == typeof(DateTime))
					{
						layout.BeginHorizontal();
						layout.Add(new Label
						{
							Text = property.Name
						});
			
						layout.Add(editorFactory.DateTimePropertyEditor(property), true);
						layout.EndHorizontal();
					}

					if (property.PropertyType == typeof(TimeSpan?) || property.PropertyType == typeof(TimeSpan))
					{
						layout.BeginHorizontal();
						layout.Add(new Label
						{
							Text = property.Name
						});
			
						layout.Add(editorFactory.TimeSpanPropertyEditor(property), true);
						layout.EndHorizontal();
					}

					if (property.PropertyType == typeof(bool))
					{
						layout.BeginHorizontal();
						layout.Add(new Label
						{
							Text = property.Name
						});
		
						layout.Add(editorFactory.BooleanPropertyEditor(property), true);
						layout.EndHorizontal();
					}

					if (typeof(IPersistent).IsAssignableFrom(property.PropertyType))
					{
						layout.BeginHorizontal();
						layout.Add(new Label
						{
							Text = property.Name
						});

						var referenceComboBox = editorFactory.ReferencePropertyEditor(property);

						layout.Add(referenceComboBox, true);

						AddReferenceButtons(layout, referenceComboBox);

						layout.EndHorizontal();
					}

					if (property.PropertyType.BaseType == typeof(Enum))
					{
						layout.BeginHorizontal();
						layout.Add(new Label
						{
							Text = property.Name
						});

						layout.Add(editorFactory.EnumPropertyEditor(property), true);
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
			layout.Add(editorFactory.ReferenceOpenButton(referenceComboBox), true);
			layout.Add(editorFactory.ReferenceClearButton(referenceComboBox), true);
			layout.EndHorizontal();
			layout.EndVertical();
		}
	}
}