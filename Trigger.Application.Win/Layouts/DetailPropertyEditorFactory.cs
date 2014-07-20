using System;
using Eto.Forms;
using Trigger.Datastore.Persistent;
using System.Linq;
using System.Reflection;

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
		}

		public TextBox StringPropertyEditor(PropertyInfo property)
		{
			var textBox = new TextBox
			{
				Text = (string)property.GetValue(Model, null)
			};
			textBox.TextChanged += (sender, e) =>
			{
				property.SetValue(Model, textBox.Text, null);
			};
			textBox.Size = new Eto.Drawing.Size(-1, -1);
			textBox.ReadOnly = !property.CanWrite;
			return textBox;
		}

		public ComboBox EnumPropertyEditor(PropertyInfo property)
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
			comboBox.SelectedKey = (property.GetValue(Model, null) as Enum).ToString();
			comboBox.SelectedValueChanged += (sender, e) =>
			{
				var current = comboBox.SelectedValue as ListItem;
				property.SetValue(Model, current.Tag, null);
			};
			return comboBox;
		}

		public ComboBox ReferencePropertyEditor(PropertyInfo property)
		{
			var lookupItem = property.PropertyType.GetCustomAttributes(typeof(System.ComponentModel.DefaultPropertyAttribute), true)
				.FirstOrDefault() as System.ComponentModel.DefaultPropertyAttribute;

			var comboBox = new ComboBox();
			var items = Dependency.DependencyMapProvider.Instance.ResolveType<IStore>().LoadAll(property.PropertyType);
			foreach (IPersistentId pi in items)
			{
				object defaultItemValue = null;
				if (lookupItem != null && !string.IsNullOrWhiteSpace(lookupItem.Name))
					defaultItemValue = pi.GetType().GetProperty(lookupItem.Name).GetValue(pi, null);

				comboBox.Items.Add(new ListItem
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
					comboBox.SelectedKey = selection.MappingId.ToString();
			}
				
			comboBox.SelectedValueChanged += (sender, e) =>
			{
				if (comboBox.SelectedValue != null)
				{
					var current = comboBox.SelectedValue as ListItem;
					if (current != null && current.Tag != null)
						property.SetValue(Model, current.Tag, null);
				}
			};
				
			comboBox.KeyDown += (sender, e) =>
			{
				if (e.Modifiers == Keys.Control && e.Key == Keys.O)
				{
					var current = comboBox.SelectedValue as ListItem;
					if (current != null)
					{
						using (var detailForm = new ModelDetailForm(current.Tag.GetType(), current.Tag as IPersistentId))
						{
							detailForm.Show();
						}
					}
				}
			};
				
			return comboBox;
		}

		public CheckBox BooleanPropertyEditor(PropertyInfo property)
		{
			var checkBox = new CheckBox
			{
				Checked = (bool)property.GetValue(Model, null)
			};
			checkBox.CheckedChanged += (sender, e) =>
			{
				property.SetValue(Model, checkBox.Checked.Value, null);
			};
			return checkBox;
		}

		public DateTimePicker DateTimePropertyEditor(PropertyInfo property)
		{
			var datePicker = new DateTimePicker
			{
				Value = (DateTime?)property.GetValue(Model, null)
			};
			datePicker.ValueChanged += (sender, e) =>
			{
				property.SetValue(Model, datePicker.Value, null);
			};
			return datePicker;
		}

		public TextBox TimeSpanPropertyEditor(PropertyInfo property)
		{
			var value = property.GetValue(Model, null) as string;
			var textBox = new TextBox
			{
				Text = value
			};
			textBox.TextChanged += (sender, e) =>
			{
				//property.SetValue(Model, textBox.Text, null);
			};
			return textBox;
		}
	}
}