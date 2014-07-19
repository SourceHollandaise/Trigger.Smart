using System;
using Eto.Forms;
using Trigger.Datastore.Persistent;
using System.Linq;

namespace Trigger.WinForms.Layout
{

	public class LayoutPropertyEditorFactory
	{
		PersistentModelBase model;

		public LayoutPropertyEditorFactory(PersistentModelBase model)
		{
			this.model = model;
			
		}

		public TextBox StringPropertyEditor(System.Reflection.PropertyInfo property)
		{
			var textBox = new TextBox
			{
				Text = (string)property.GetValue(model, null)
			};
			textBox.TextChanged += (sender, e) =>
			{
				property.SetValue(model, textBox.Text, null);
			};
			textBox.Size = new Eto.Drawing.Size(-1, -1);
			textBox.ReadOnly = !property.CanWrite;
			return textBox;
		}

		public ComboBox EnumPropertyEditor(System.Reflection.PropertyInfo property)
		{
			var comboBox = new ComboBox();

			var enumValues = Enum.GetValues(property.PropertyType);
			foreach (var value in enumValues)
				comboBox.Items.Add(new ListItem
				{
					Key = value.ToString(),
					Text = value.ToString(),
					Tag = value
				});
			comboBox.SelectedKey = (property.GetValue(model, null) as Enum).ToString();
			comboBox.SelectedValueChanged += (sender, e) =>
			{
				var current = comboBox.SelectedValue as ListItem;
				property.SetValue(model, current.Tag, null);
			};
			return comboBox;
		}

		public ComboBox ReferencePropertyEditor(System.Reflection.PropertyInfo property)
		{
			var lookupItem = property.PropertyType.GetCustomAttributes(typeof(System.ComponentModel.DefaultPropertyAttribute), true).FirstOrDefault() as System.ComponentModel.DefaultPropertyAttribute;

			var comboBox = new ComboBox();
			var items = Dependency.DependencyMapProvider.Instance.ResolveType<IStore>().LoadAll(property.PropertyType);
			foreach (PersistentModelBase pi in items)
			{
				var defaultItemValue = pi.GetType().GetProperty(lookupItem.Name).GetValue(pi, null);
				comboBox.Items.Add(new ListItem()
				{
					Key = pi.MappingId.ToString(),
					Text = defaultItemValue != null ? defaultItemValue as string : null,
					Tag = pi
				});
			}
			var value = property.GetValue(model, null);
			if (value != null)
			{
				var selection = (property.GetValue(model, null) as PersistentModelBase);
				if (selection != null && selection.MappingId != null)
					comboBox.SelectedKey = selection.MappingId.ToString();
			}
			comboBox.SelectedValueChanged += (sender, e) =>
			{
				var current = comboBox.SelectedValue as ListItem;
				property.SetValue(model, current.Tag, null);
			};
			return comboBox;
		}

		public CheckBox BooleanPropertyEditor(System.Reflection.PropertyInfo property)
		{
			var checkBox = new CheckBox
			{
				Checked = (bool)property.GetValue(model, null)
			};
			checkBox.CheckedChanged += (sender, e) =>
			{
				property.SetValue(model, checkBox.Checked.Value, null);
			};
			return checkBox;
		}

		public DateTimePicker DateTimePropertyEditor(System.Reflection.PropertyInfo property)
		{
			var datePicker = new DateTimePicker
			{
				Value = (DateTime?)property.GetValue(model, null)
			};
			datePicker.ValueChanged += (sender, e) =>
			{
				property.SetValue(model, datePicker.Value, null);
			};
			return datePicker;
		}

		public TextBox TimeSpanPropertyEditor(System.Reflection.PropertyInfo property)
		{
			var textBox = new TextBox
			{

				//Text = (string)property.GetValue(model, null)
			};
			textBox.TextChanged += (sender, e) =>
			{
				//property.SetValue(model, Convert.ToDateTime(textBox.Text), null);
			};
			return textBox;
		}
	}
}