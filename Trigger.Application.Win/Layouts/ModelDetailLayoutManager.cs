using System;
using Eto.Forms;
using Trigger.Datastore.Persistent;

namespace Trigger.Application.Win.Layouts
{
	public class ModelDetailLayoutManager
	{
		readonly IStore store = Dependency.DependencyMapProvider.Instance.ResolveType<IStore>();

		public DynamicLayout GetLayout(PersistentModelBase model)
		{
			DynamicLayout layout = new DynamicLayout();
			var properties = model.GetType().GetProperties();

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
						layout.Add(textBox, true);
					
						layout.EndHorizontal();
					}

					if (property.PropertyType == typeof(DateTime?) || property.PropertyType == typeof(DateTime))
					{
						layout.BeginHorizontal();
						layout.Add(new Label
						{
							Text = property.Name
						});
						var datePicker = new DateTimePicker
						{
							Value = (DateTime?)property.GetValue(model, null)
						};

						datePicker.ValueChanged += (sender, e) =>
						{
							property.SetValue(model, datePicker.Value, null);
						};

						layout.Add(datePicker, true);
						layout.EndHorizontal();
					}

					if (property.PropertyType == typeof(TimeSpan?) || property.PropertyType == typeof(TimeSpan))
					{
						layout.BeginHorizontal();
						layout.Add(new Label
						{
							Text = property.Name
						});
						var textBox = new TextBox
						{
							//Text = (string)property.GetValue(model, null)
						};

						textBox.TextChanged += (sender, e) =>
						{
							//property.SetValue(model, Convert.ToDateTime(textBox.Text), null);
						};

						layout.Add(textBox, true);
						layout.EndHorizontal();
					}

					if (property.PropertyType == typeof(bool))
					{
						layout.BeginHorizontal();
						layout.Add(new Label
						{
							Text = property.Name
						});
						var checkBox = new CheckBox
						{
							Checked = (bool)property.GetValue(model, null)
						};

						checkBox.CheckedChanged += (sender, e) =>
						{
							property.SetValue(model, checkBox.Checked.Value, null);
						};

						layout.Add(checkBox, true);
						layout.EndHorizontal();
					}

					if (property.PropertyType.BaseType == typeof(PersistentModelBase))
					{
						layout.BeginHorizontal();
						layout.Add(new Label
						{
							Text = property.Name
						});
						var comboBox = new ComboBox();

						var items = store.LoadAll(property.PropertyType);
						foreach (PersistentModelBase pi in items)
							comboBox.Items.Add(new ListItem()
							{
								Key = pi.MappingId.ToString(),
								Text = pi.GetRepresentation(),
								Tag = pi
							});

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

						layout.Add(comboBox, true);
						layout.EndHorizontal();
					}

					if (property.PropertyType.BaseType == typeof(Enum))
					{
						layout.BeginHorizontal();
						layout.Add(new Label
						{
							Text = property.Name
						});

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


						layout.Add(comboBox, true);
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