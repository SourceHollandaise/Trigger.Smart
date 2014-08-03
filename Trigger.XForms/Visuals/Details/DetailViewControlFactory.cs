using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using Eto.Drawing;
using Eto.Forms;
using Trigger.XStorable.DataStore;
using Trigger.XStorable.Dependency;

namespace Trigger.XForms.Visuals
{
    public class DetailViewControlFactory
    {
        Dictionary<string, Control> controlCollection = new Dictionary<string, Control>();

        protected IStorable Model
        {
            get;
            set;
        }

        public DetailViewControlFactory(IStorable model)
        {
            this.Model = model;
            if (Model != null)
            {
                Model.PropertyChanged += (sender, e) =>
                {
                    HandleBindings(Model.GetType().GetProperty(e.PropertyName));
                };
            }
        }

        public Control GetControl(PropertyInfo property, ViewItemDescription viewItem)
        {
            if (property.PropertyType == typeof(string))
            {
                var fieldFileDataAttribute = property.FindAttribute<FieldFileDataAttribute>();
                var fieldImageDataAttribute = property.FindAttribute<FieldImageDataAttribute>();
                var fieldTextAreaAttribute = property.FindAttribute<FieldTextAreaAttribute>();

                if (fieldImageDataAttribute != null)
                {
                    if (fieldImageDataAttribute.Thumbnail)
                        return ImageViewThumbnailPropertyEditor(property);
                    return ImageViewPropertyEditor(property);
                }

                if (fieldTextAreaAttribute != null)
                    return TextAreaPropertyEditor(property);

                if (fieldFileDataAttribute != null)
                    return FilePreviewPropertyEditor(property);
                return StringPropertyEditor(property);

            }

            if (property.PropertyType == typeof(Color))
                return ImageViewPropertyEditor(property);


            if (property.PropertyType == typeof(Image))
                return ImageViewPropertyEditor(property);

            if (property.PropertyType == typeof(DateTime?) || property.PropertyType == typeof(DateTime))
            {
                return DateTimePropertyEditor(property);
            }

            if (property.PropertyType == typeof(TimeSpan?) || property.PropertyType == typeof(TimeSpan))
            {
                return TimeSpanPropertyEditor(property);
            }

            if (property.PropertyType == typeof(bool))
            {
                return BooleanPropertyEditor(property);
            }

            if (typeof(IStorable).IsAssignableFrom(property.PropertyType))
            {
                return ReferencePropertyEditor(property);
            }

            if (property.PropertyType == typeof(int) || property.PropertyType == typeof(double) || property.PropertyType == typeof(decimal))
            {
                return NumberPropertyEditor(property);
            }

            if (property.PropertyType.BaseType == typeof(Enum))
            {
                return EnumPropertyEditor(property);
            }

            var linkedListAttribute = property.FindAttribute<LinkedListAttribute>();
            if (linkedListAttribute != null)
            {
                var value = property.GetValue(Model, null);
                if (value is IEnumerable<IStorable>)
                {
                    if (viewItem.ListMode == ListPropertyMode.List)
                    {
                        var descriptorType = ListViewDescriptorProvider.GetDescriptor(linkedListAttribute.LinkType);

                        if (descriptorType != null)
                        {
                            var descriptor = Activator.CreateInstance(descriptorType) as IListViewDescriptor;

                            var control = new ListViewBuilder(descriptor, linkedListAttribute.LinkType, false, value as IEnumerable<IStorable>).GetContent();

                            if (control != null)
                            {
                                control.MouseDoubleClick += (sender, e) =>
                                {
                                    if ((control as GridView).SelectedItem != null)
                                        DetailViewExtensions.ShowDetailView((control as GridView).SelectedItem as IStorable);
                                };
                            }
                            return control;
                        }
                    }

                    if (viewItem.ListMode == ListPropertyMode.Button)
                    {
                        var control = new Button
                        {
                            Tag = value
                        };
                            
                        var displayNameAttribute = property.FindAttribute<DisplayNameAttribute>();

                        control.Text = displayNameAttribute != null ? displayNameAttribute.DisplayName : property.Name;

                        control.Click += (sender, e) =>
                        {
                           
                        };

                        return control;
                    }
                }
            }

            return null;
        }

        void HandleBindings(PropertyInfo property)
        {
            if (property == null)
                return;

            if (!controlCollection.ContainsKey(property.Name))
                return;

            var control = controlCollection[property.Name];
           
            if (control is WebView)
            {
                var storeConfig = DependencyMapProvider.Instance.ResolveInstance<IStoreConfiguration>();
                var fileName = (string)property.GetValue(Model, null);
                if (!string.IsNullOrEmpty(fileName))
                {
                    var path = Path.Combine(storeConfig.DocumentStoreLocation, fileName);
                    if (File.Exists(path))
                        ((WebView)control).Url = new Uri(path, UriKind.RelativeOrAbsolute);
                }
            }

            if (control is ImageView)
                ((ImageView)control).Image = property.GetValue(Model, null) as Image;

            if (control is NumericUpDown)
                ((NumericUpDown)control).Value = Convert.ToDouble(property.GetValue(Model, null));
    
            if (control is TextBox)
                ((TextBox)control).Text = (string)property.GetValue(Model, null);

            if (control is DateTimePicker)
                ((DateTimePicker)control).Value = (DateTime?)property.GetValue(Model, null);

            if (control is CheckBox)
                ((CheckBox)control).Checked = (bool?)property.GetValue(Model, null);

            if (control is ComboBox)
            {
                var value = (property.GetValue(Model, null));

                if (typeof(IStorable).IsAssignableFrom(property.PropertyType))
                {
                    if (value != null)
                        ((ComboBox)control).SelectedKey = (string)(value as IStorable).MappingId;
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

        ImageView ImageViewPropertyEditor(PropertyInfo property)
        {
            var imageView = new ImageView();
            imageView.Size = new Size(-1, -1);

            var value = (string)property.GetValue(Model, null);
            if (!string.IsNullOrWhiteSpace(value))
            {
                var file = value.GetValidPath();
                if (file != null)
                {
                    var image = new Bitmap(file);

                    imageView.Size = image.Size;
                    imageView.Image = image;
                }
            }

            return imageView;
        }

        ImageView ImageViewThumbnailPropertyEditor(PropertyInfo property)
        {
            var imageView = new ImageView();
            imageView.Size = new Size(96, 96);

            var value = (string)property.GetValue(Model, null);
            if (!string.IsNullOrWhiteSpace(value))
            {
                var file = value.GetValidPath();
                if (file != null)
                {
                    var image = new Bitmap(file);
                    var thumbNail = new Bitmap(image, 96, 96);

                    imageView.Image = thumbNail;
                }
            }

            return imageView;
        }

        WebView FilePreviewPropertyEditor(PropertyInfo property)
        {
            var webView = new WebView();

            webView.Size = new Size(-1, -1);

            var value = (string)property.GetValue(Model, null);
            if (!string.IsNullOrWhiteSpace(value))
            {
                var file = value.GetValidPath();
                if (file != null)
                {
                    webView.Url = new Uri(file, UriKind.RelativeOrAbsolute);

                }
            }
            webView.BrowserContextMenuEnabled = true;

            return webView;
        }

        bool IsEnabled(PropertyInfo property)
        {
            var attribute = property.FindAttribute<ReadOnlyAttribute>(); 
            return attribute == null || !attribute.IsReadOnly;
        }

        TextBox StringPropertyEditor(PropertyInfo property)
        {

            var control = new TextBox
            {
                Text = (string)property.GetValue(Model, null),
            };

            control.TextChanged += (sender, e) =>
            {
                if (!control.Text.Equals(control.PlaceholderText))
                    property.SetValue(Model, control.Text, null);
            };
            control.Size = new Size(-1, -1);
            control.Enabled = IsEnabled(property);
            controlCollection.Add(property.Name, control);

            return control;
        }

        TextArea TextAreaPropertyEditor(PropertyInfo property)
        {
            var control = new TextArea
            {
                Text = (string)property.GetValue(Model, null),
            };
                
            control.TextChanged += (sender, e) =>
            {
                property.SetValue(Model, control.Text, null);
            };
            control.Size = new Size(-1, -1);
            control.Enabled = IsEnabled(property);
            controlCollection.Add(property.Name, control);

            return control;
        }

        ComboBox EnumPropertyEditor(PropertyInfo property)
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
            controlCollection.Add(property.Name, control);
            return control;
        }

        Button ReferenceOpenButton(ComboBox control)
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

        Button ReferenceClearButton(ComboBox control)
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

        ComboBox ReferencePropertyEditor(PropertyInfo property)
        {
            var attribute = property.PropertyType.FindAttribute<DefaultPropertyAttribute>();

            var control = new ComboBox();
            var items = DependencyMapProvider.Instance.ResolveType<IStore>().LoadAll(property.PropertyType).ToList();
            foreach (IStorable pi in items)
            {
                object defaultItemValue = null;
                if (attribute != null && !string.IsNullOrWhiteSpace(attribute.Name))
                {
                    defaultItemValue = pi.GetType().GetProperty(attribute.Name).GetValue(pi, null);
                }

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
                var selection = (property.GetValue(Model, null) as IStorable);
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
            controlCollection.Add(property.Name, control);
            return control;
        }

        CheckBox BooleanPropertyEditor(PropertyInfo property)
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
            controlCollection.Add(property.Name, control);
            return control;
        }

        NumericUpDown NumberPropertyEditor(PropertyInfo property)
        {
            var control = new NumericUpDown
            {
                Value = Convert.ToDouble(property.GetValue(Model, null))
            };

            control.ValueChanged += (sender, e) =>
            {
                if (property.PropertyType == typeof(int))
                    property.SetValue(Model, Convert.ToInt32(control.Value), null);

                if (property.PropertyType == typeof(double))
                    property.SetValue(Model, Convert.ToDouble(control.Value), null);

                if (property.PropertyType == typeof(decimal))
                    property.SetValue(Model, Convert.ToDecimal(control.Value), null);
            };
            control.Size = new Size(-1, -1);
            control.Enabled = IsEnabled(property);
            controlCollection.Add(property.Name, control);

            return control;
        }

        DateTimePicker DateTimePropertyEditor(PropertyInfo property)
        {
            var control = new DateTimePicker
            {
                Value = (DateTime?)property.GetValue(Model, null),
                Mode = DateTimePickerMode.DateTime,
                MinDate = new DateTime(1970, 1, 1),
            };
            control.ValueChanged += (sender, e) =>
            {
                property.SetValue(Model, control.Value, null);
            };
            control.Enabled = IsEnabled(property);
            controlCollection.Add(property.Name, control);
            return control;
        }

        TextBox TimeSpanPropertyEditor(PropertyInfo property)
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
            controlCollection.Add(property.Name, control);
            return control;
        }

        void OpenReference(ComboBox control)
        {
            var current = control.SelectedValue as ListItem;
            if (current != null)
                DetailViewExtensions.ShowDetailView(current.Tag as IStorable);
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