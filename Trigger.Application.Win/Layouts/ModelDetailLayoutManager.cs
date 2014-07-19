using System;
using Eto.Forms;
using Trigger.Datastore.Persistent;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace Trigger.WinForms.Layout
{
	public class ModelDetailLayoutManager
	{
		protected IPersistentId Model
		{
			get;
			set;
		}

		public ModelDetailLayoutManager(IPersistentId model)
		{
			this.Model = model;
		}

		public DynamicLayout GetLayout()
		{
			var layout = new DynamicLayout();
			var properties = Model.GetType().GetProperties();
			var editorFactory = new DetailPropertyEditorFactory(Model);

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