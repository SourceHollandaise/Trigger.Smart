using System;
using Eto.Forms;
using Trigger.Datastore.Persistent;

namespace Trigger.WinForms.Layout
{
	public class ModelDetailLayoutManager
	{
		public DynamicLayout GetLayout(PersistentModelBase model)
		{
			var layout = new DynamicLayout();
			var properties = model.GetType().GetProperties();
			var editorFactory = new LayoutPropertyEditorFactory(model);

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

					if (property.PropertyType.BaseType == typeof(PersistentModelBase))
					{
						layout.BeginHorizontal();
						layout.Add(new Label
						{
							Text = property.Name
						});

						layout.Add(editorFactory.ReferencePropertyEditor(property), true);
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
	}
}