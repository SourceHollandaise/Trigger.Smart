using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Eto.Forms;
using Trigger.Datastore.Persistent;
using Eto.Drawing;

namespace Trigger.WinForms.Layout
{
	public class DetailPropertyEditorFactory
	{
		protected IPersistent Model
		{
			get;
			set;
		}

		public DetailPropertyEditorFactory(IPersistent model)
		{
			this.Model = model;

			Model.PropertyChanged += (sender, e) =>
			{
				HandleBindings(Model.GetType().GetProperty(e.PropertyName));
			};
		}

		void HandleBindings(PropertyInfo property)
		{
			var control = Bindings[property.Name];

			if (control is TextBox)
				((TextBox)control).Text = (string)property.GetValue(Model, null);

			if (control is DateTimePicker)
				((DateTimePicker)control).Value = (DateTime?)property.GetValue(Model, null);

			if (control is CheckBox)
				((CheckBox)control).Checked = (bool?)property.GetValue(Model, null);

			if (control is ComboBox)
			{
				var value = (property.GetValue(Model, null));

				if (typeof(IPersistent).IsAssignableFrom(property.PropertyType))
				{
					if (value != null)
						((ComboBox)control).SelectedKey = (string)(value as IPersistent).MappingId;
					else
						((ComboBox)control).SelectedValue = null;
				}

				if (property.PropertyType == typeof(Enum))
				{
					if (value != null)
						((ComboBox)control).SelectedKey = (string)value;
					else
						((ComboBox)control).SelectedValue = null;
				}
			}
		}

		Dictionary<string, Control> Bindings = new Dictionary<string, Control>();

		bool IsEnabled(PropertyInfo property)
		{
			var attribute = property.GetCustomAttributes(typeof(System.ComponentModel.ReadOnlyAttribute), true)
				.FirstOrDefault() as System.ComponentModel.ReadOnlyAttribute;

			if (attribute == null)
				return true;

			return !attribute.IsReadOnly;
		}

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
			control.Size = new Size(-1, -1);
			control.Enabled = IsEnabled(property);
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
			control.Enabled = IsEnabled(property);
			Bindings.Add(property.Name, control);
			return control;
		}

		public Button ReferenceOpenButton(ComboBox control)
		{
			var button = new Button();
			button.Text = "Open";
			button.Image = ImageExtensions.GetImage("Edit32.png", 12);
			button.ImagePosition = ButtonImagePosition.Left;

			button.Click += (sender, e) =>
			{
				OpenReference(control);
			};

			return button;
		}

		public Button ReferenceClearButton(ComboBox control)
		{
			var button = new Button();
			button.Text = "Clear";
			button.Image = ImageExtensions.GetImage("Delete32.png", 12);
			button.ImagePosition = ButtonImagePosition.Left;
			button.Click += (sender, e) =>
			{
				ClearReference(control);
			};

			return button;
		}

		public ComboBox ReferencePropertyEditor(PropertyInfo property)
		{
			var attribute = property.PropertyType.GetCustomAttributes(typeof(System.ComponentModel.DefaultPropertyAttribute), true).FirstOrDefault() as System.ComponentModel.DefaultPropertyAttribute;

			var control = new ComboBox();
			var items = Dependency.DependencyMapProvider.Instance.ResolveType<IStore>().LoadAll(property.PropertyType).ToList();
			foreach (IPersistent pi in items)
			{
				object defaultItemValue = null;
				if (attribute != null && !string.IsNullOrWhiteSpace(attribute.Name))
					defaultItemValue = pi.GetType().GetProperty(attribute.Name).GetValue(pi, null);

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
				var selection = (property.GetValue(Model, null) as IPersistent);
				if (selection != null && selection.MappingId != null)
				{
					control.SelectedKey = selection.MappingId.ToString();
				}
			}
				
			control.SelectedValueChanged += (sender, e) =>
			{
				if (control.SelectedValue != null)
				{
					var current = control.SelectedValue as ListItem;
					if (current != null && current.Tag != null)
					{
						property.SetValue(Model, current.Tag, null);
					}
				}
				else
					property.SetValue(Model, null, null);
			};
				
			control.KeyDown += (sender, e) =>
			{
				if (e.Modifiers == Keys.Control && e.Key == Keys.O)
					OpenReference(control);

				if (e.Modifiers == Keys.Control && e.Key == Keys.Backspace)
					ClearReference(control);
			};
			control.Enabled = IsEnabled(property);
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
			control.Enabled = IsEnabled(property);
			Bindings.Add(property.Name, control);
			return control;
		}

		public DateTimePicker DateTimePropertyEditor(PropertyInfo property)
		{
			var control = new DateTimePicker
			{
				Value = (DateTime?)property.GetValue(Model, null),
				Mode = DateTimePickerMode.DateTime
			};
			control.ValueChanged += (sender, e) =>
			{
				property.SetValue(Model, control.Value, null);
			};
			control.Enabled = IsEnabled(property);
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
			control.Enabled = IsEnabled(property);
			Bindings.Add(property.Name, control);
			return control;
		}

		void OpenReference(ComboBox control)
		{
			var current = control.SelectedValue as ListItem;
			if (current != null)
				WindowManager.ShowDetailView(current.Tag as IPersistent);
		}

		void  ClearReference(ComboBox control)
		{
			var current = control.SelectedValue as ListItem;
			if (current != null)
			{
				control.SelectedValue = null;
				control.SelectedKey = null;

			}
		}
	}
}