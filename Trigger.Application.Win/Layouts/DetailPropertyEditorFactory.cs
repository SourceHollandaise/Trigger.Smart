using System;
using Eto.Forms;
using Trigger.Datastore.Persistent;
using System.Linq;
using System.Reflection;
using Eto;
using System.Collections.Generic;

namespace Trigger.WinForms.Layout
{
	public class DetailPropertyEditorFactory
	{
		protected IPersistentId Model
		{
			get;
			set;
		}

		public DetailPropertyEditorFactory(IPersistentId model)
		{
			this.Model = model;

			Model.PropertyChanged += (sender, e) =>
			{
				var prop = Model.GetType().GetProperty(e.PropertyName);

				var control = Bindings[prop.Name];

				if (control is TextBox)
					((TextBox)control).Text = (string)prop.GetValue(Model, null);

				if (control is DateTimePicker)
					((DateTimePicker)control).Value = (DateTime?)prop.GetValue(Model, null);

				if (control is CheckBox)
					((CheckBox)control).Checked = (bool?)prop.GetValue(Model, null);
					
			};
		}

		Dictionary<string, Control> Bindings = new Dictionary<string, Control>();

		public TextBox StringPropertyEditor(PropertyInfo property)
		{
			var control = new TextBox
			{
				Text = (string)property.GetValue(Model, null)
			};
			control.TextChanged += (sender, e) =>
			{
				property.SetValue(Model, control.Text, null);
			};
			control.Size = new Eto.Drawing.Size(-1, -1);
			control.ReadOnly = !property.CanWrite;
			Bindings.Add(property.Name, control);

			return control;
		}

		public ComboBox EnumPropertyEditor(PropertyInfo property)
		{
			var control = new ComboBox();

			var enumValues = Enum.GetValues(property.PropertyType);
			foreach (var value in enumValues)
				control.Items.Add(new ListItem
				{
					Key = value.ToString(),
					Text = value.ToString(),
					Tag = value
				});
			control.SelectedKey = (property.GetValue(Model, null) as Enum).ToString();
			control.SelectedValueChanged += (sender, e) =>
			{
				var current = control.SelectedValue as ListItem;
				property.SetValue(Model, current.Tag, null);
			};
			Bindings.Add(property.Name, control);
			return control;
		}

		public ComboBox ReferencePropertyEditor(PropertyInfo property)
		{
			var lookupItem = property.PropertyType.GetCustomAttributes(typeof(System.ComponentModel.DefaultPropertyAttribute), true)
				.FirstOrDefault() as System.ComponentModel.DefaultPropertyAttribute;

			var control = new ComboBox();
			var items = Dependency.DependencyMapProvider.Instance.ResolveType<IStore>().LoadAll(property.PropertyType).ToList();
			foreach (IPersistentId pi in items)
			{
				object defaultItemValue = null;
				if (lookupItem != null && !string.IsNullOrWhiteSpace(lookupItem.Name))
					defaultItemValue = pi.GetType().GetProperty(lookupItem.Name).GetValue(pi, null);

				control.Items.Add(new ListItem
				{
					Key = pi.MappingId.ToString(),
					Text = defaultItemValue != null ? defaultItemValue as string : null,
					Tag = pi
				});
			}
			var value = property.GetValue(Model, null);
			if (value != null)
			{
				var selection = (property.GetValue(Model, null) as IPersistentId);
				if (selection != null && selection.MappingId != null)
					control.SelectedKey = selection.MappingId.ToString();
			}
				
			control.SelectedValueChanged += (sender, e) =>
			{
				if (control.SelectedValue != null)
				{
					var current = control.SelectedValue as ListItem;
					if (current != null && current.Tag != null)
						property.SetValue(Model, current.Tag, null);
				}
			};
				
			control.KeyDown += (sender, e) =>
			{
				if (e.Modifiers == Keys.Control && e.Key == Keys.O)
				{
					var current = control.SelectedValue as ListItem;
					if (current != null)
					{
						var detailForm = new ModelDetailForm(current.Tag.GetType(), current.Tag as IPersistentId);
						detailForm.Show();
					}
				}
			};
			Bindings.Add(property.Name, control);
			return control;
		}

		public CheckBox BooleanPropertyEditor(PropertyInfo property)
		{
			var control = new CheckBox
			{
				Checked = (bool)property.GetValue(Model, null)
			};
			control.CheckedChanged += (sender, e) =>
			{
				property.SetValue(Model, control.Checked.Value, null);
			};
			Bindings.Add(property.Name, control);
			return control;
		}

		public DateTimePicker DateTimePropertyEditor(PropertyInfo property)
		{
			var control = new DateTimePicker
			{
				Value = (DateTime?)property.GetValue(Model, null)
			};
			control.ValueChanged += (sender, e) =>
			{
				property.SetValue(Model, control.Value, null);
			};
			Bindings.Add(property.Name, control);
			return control;
		}

		public TextBox TimeSpanPropertyEditor(PropertyInfo property)
		{
			var value = property.GetValue(Model, null) as string;
			var control = new TextBox
			{
				Text = value
			};
			control.TextChanged += (sender, e) =>
			{
				//property.SetValue(Model, textBox.Text, null);
			};
			Bindings.Add(property.Name, control);
			return control;
		}
	}
}