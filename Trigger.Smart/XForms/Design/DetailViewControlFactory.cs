using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using Eto.Drawing;
using Eto.Forms;
using XForms.Store;
using XForms.Model;
using XForms.Dependency;

namespace XForms.Design
{
    public class DetailViewControlFactory
    {
        Dictionary<string, Control> controlCollection = new Dictionary<string, Control>();

        readonly IStorable currentObject;

        public DetailViewControlFactory(IStorable currentObject)
        {
            this.currentObject = currentObject;
            if (currentObject != null)
            {
                currentObject.PropertyChanged += (sender, e) =>
                {
                    if (!string.IsNullOrWhiteSpace(e.PropertyName))
                        HandleBindings(currentObject.GetType().GetProperty(e.PropertyName));
                };
            }
        }

        public Control GetControl(PropertyInfo property, ViewItemDescription viewItem)
        {
            if (property == null)
                return null;

            if (property.PropertyType == typeof(string))
            {
                var fieldFileDataAttribute = property.FindAttribute<FieldFileDataAttribute>();
                var fieldImageDataAttribute = property.FindAttribute<FieldImageDataAttribute>();
                var fieldTextAreaAttribute = property.FindAttribute<FieldTextAreaAttribute>();

                if (fieldImageDataAttribute != null)
                {
                    if (fieldImageDataAttribute.Thumbnail)
                        return ImageViewThumbnailPropertyEditor(property, fieldImageDataAttribute.DefaultWidth, fieldImageDataAttribute.DefaultHeight);
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
                return DateTimePropertyEditor(property);

            if (property.PropertyType == typeof(TimeSpan?) || property.PropertyType == typeof(TimeSpan))
                return TimeSpanPropertyEditor(property);

            if (property.PropertyType == typeof(bool))
                return BooleanPropertyEditor(property);

            if (typeof(IStorable).IsAssignableFrom(property.PropertyType))
                return ReferencePropertyEditor(property);

            if (property.PropertyType == typeof(int) || property.PropertyType == typeof(double) || property.PropertyType == typeof(decimal))
                return NumberPropertyEditor(property);

            if (property.PropertyType.BaseType == typeof(Enum))
                return EnumPropertyEditor(property);

            var linkedListAttribute = property.FindAttribute<LinkedListAttribute>();
            if (linkedListAttribute != null)
            {
                var value = property.GetValue(currentObject, null);
                if (value is IEnumerable<IStorable>)
                {
                    if (viewItem.ListMode == ListPropertyMode.List)
                    {
                        var descriptorType = ListViewDescriptorProvider.GetDescriptor(linkedListAttribute.LinkType);

                        if (descriptorType != null)
                        {
                            var descriptor = Activator.CreateInstance(descriptorType) as IListViewDescriptor;

                            var dataSet = value as IEnumerable<IStorable>;
                            if (descriptor.Filter != null)
                                dataSet = dataSet.Where(descriptor.Filter);

                            Control control = null;

                            if (descriptor.ShowListDetailViewForLinkedLists)
                            {
                                if (dataSet == null || !dataSet.Any())
                                    return new DynamicLayout();

                                control = new ListDetailViewBuilder(descriptor, linkedListAttribute.LinkType, dataSet).GetContent();

                            }
                            else
                            {
                                control = new ListViewBuilder(descriptor, linkedListAttribute.LinkType, false, dataSet).GetContent();

                                if (control != null)
                                {
                                    control.MouseDoubleClick += (sender, e) =>
                                    {
                                        if ((control as GridView).SelectedItem != null)
                                            ((control as GridView).SelectedItem as IStorable).ShowDetailContentEmbedded();
                                    };
                                }
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
                            //TODO: Open in current Template? Open in new Template?
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
                var storeConfig = MapProvider.Instance.ResolveInstance<IStoreConfiguration>();
                var fileName = (string)property.GetValue(currentObject, null);
                if (!string.IsNullOrEmpty(fileName))
                {
                    var path = Path.Combine(storeConfig.DocumentStoreLocation, fileName);
                    if (File.Exists(path))
                        ((WebView)control).Url = new Uri(path, UriKind.RelativeOrAbsolute);
                }
            }

            if (control is ImageView)
                ((ImageView)control).Image = property.GetValue(currentObject, null) as Image;

            if (control is NumericUpDown)
                ((NumericUpDown)control).Value = Convert.ToDouble(property.GetValue(currentObject, null));
    
            if (control is TextBox)
                ((TextBox)control).Text = (string)property.GetValue(currentObject, null);

            if (control is DateTimePicker)
            {
                if (((DateTimePicker)control).Mode == DateTimePickerMode.DateTime)
                    ((DateTimePicker)control).Value = (DateTime?)property.GetValue(currentObject, null);

                if (((DateTimePicker)control).Mode == DateTimePickerMode.Time)
                {
                    TimeSpan? value = (TimeSpan?)property.GetValue(currentObject, null);

                    ((DateTimePicker)control).Value = new DateTime().Add(value ?? new TimeSpan());
                }
            }

            if (control is CheckBox)
                ((CheckBox)control).Checked = (bool?)property.GetValue(currentObject, null);

            if (control is ComboBox)
            {
                var value = (property.GetValue(currentObject, null));

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

            var value = (string)property.GetValue(currentObject, null);
            if (!string.IsNullOrWhiteSpace(value))
            {
                var file = value.GetValidPath();
                if (file != null)
                {
                    var image = new Bitmap(file);
                    imageView.Image = image;
                }
            }

            return imageView;
        }

        ImageView ImageViewThumbnailPropertyEditor(PropertyInfo property, int defaultWidth = 128, int defaultHeight = 128)
        {
            var imageView = new ImageView();
            imageView.Size = new Size(defaultWidth, defaultHeight);

            var value = (string)property.GetValue(currentObject, null);
            if (!string.IsNullOrWhiteSpace(value))
            {
                var file = value.GetValidPath();
                if (file != null)
                {
                    var image = new Bitmap(file);
                    var size = GetScaledSize(image.Size, defaultWidth, defaultHeight);
                   
                    var thumbnail = new Bitmap(image, size.Width, size.Height, ImageInterpolation.High);

                    imageView.Image = thumbnail;

                    imageView.MouseDoubleClick += (sender, e) =>
                    {
                        if (currentObject != null)
                            currentObject.ShowDetailContentEmbedded();
                    };
                }
            }

           
            return imageView;
        }

        Size GetScaledSize(Size originaleSize, int maxHeight, int maxWidth)
        {
            double resizeWidth = originaleSize.Width;
            double resizeHeight = originaleSize.Height;

            double aspect = resizeWidth / resizeHeight;

            if (resizeWidth > maxHeight)
            {
                resizeWidth = maxWidth;
                resizeHeight = resizeWidth / aspect;
            }
            if (resizeHeight > maxHeight)
            {
                aspect = resizeWidth / resizeHeight;
                resizeHeight = maxHeight;
                resizeWidth = resizeHeight * aspect;
            }

            return new Size((int)resizeWidth, (int)resizeHeight);
        }

        WebView FilePreviewPropertyEditor(PropertyInfo property)
        {
            var webView = new WebView();

            webView.Size = new Size(-1, -1);

            var value = (string)property.GetValue(currentObject, null);
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

        TextBox StringPropertyEditor(PropertyInfo property)
        {

            var control = new TextBox
            {
                Text = (string)property.GetValue(currentObject, null),
            };

            control.TextChanged += (sender, e) =>
            {
                if (!control.Text.Equals(control.PlaceholderText))
                    property.SetValue(currentObject, control.Text, null);
            };
            control.Size = new Size(-1, -1);
           
            controlCollection.Add(property.Name, control);

            return control;
        }

        TextArea TextAreaPropertyEditor(PropertyInfo property)
        {
            var control = new TextArea
            {
                Text = (string)property.GetValue(currentObject, null),
            };
                
            control.TextChanged += (sender, e) =>
            {
                property.SetValue(currentObject, control.Text, null);
            };
            control.Size = new Size(-1, -1);

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
            control.SelectedKey = (property.GetValue(currentObject, null) as Enum).ToString();
            control.SelectedValueChanged += (sender, e) =>
            {
                var current = control.SelectedValue as ListItem;
                property.SetValue(currentObject, current.Tag, null);
            };
           
            control.Size = new Size(-1, -1);
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
            var defaultPropertyAttribute = property.PropertyType.FindAttribute<DefaultPropertyAttribute>();

            var control = new ComboBox();

            var lookupItems = MapProvider.Instance.ResolveType<IStore>().LoadAll(property.PropertyType);

            if (defaultPropertyAttribute != null)
                lookupItems = lookupItems.OrderByProperty(defaultPropertyAttribute.Name);

            foreach (IStorable pi in lookupItems.ToList())
            {
                object defaultItemValue = null;

                if (defaultPropertyAttribute != null && !string.IsNullOrWhiteSpace(defaultPropertyAttribute.Name))
                {
                    defaultItemValue = pi.GetType().GetProperty(defaultPropertyAttribute.Name).GetValue(pi, null);
                }

                control.Items.Add(new ListItem
                {
                    Key = pi.MappingId.ToString(),
                    Text = defaultItemValue != null ? defaultItemValue as string : null,
                    Tag = pi
                });
            }

            var value = property.GetValue(currentObject, null);

            if (value != null)
            {
                var selection = (property.GetValue(currentObject, null) as IStorable);
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
                        property.SetValue(currentObject, current.Tag, null);
                    }
                }
                else
                    property.SetValue(currentObject, null, null);
            };
				
            control.KeyDown += (sender, e) =>
            {
                if (e.Modifiers == Keys.Control && e.Key == Keys.O)
                    OpenReference(control);

                if (e.Modifiers == Keys.Control && e.Key == Keys.Backspace)
                    ClearReference(control);
            };

            control.Size = new Size(-1, -1);
            controlCollection.Add(property.Name, control);
            return control;
        }

        CheckBox BooleanPropertyEditor(PropertyInfo property)
        {
            var control = new CheckBox
            {
                Checked = (bool)property.GetValue(currentObject, null)
            };
            control.CheckedChanged += (sender, e) =>
            {
                property.SetValue(currentObject, control.Checked.Value, null);
            };

            controlCollection.Add(property.Name, control);
            return control;
        }

        NumericUpDown NumberPropertyEditor(PropertyInfo property)
        {
            var control = new NumericUpDown
            {
                Value = Convert.ToDouble(property.GetValue(currentObject, null))
            };

            control.ValueChanged += (sender, e) =>
            {
                if (property.PropertyType == typeof(int))
                    property.SetValue(currentObject, Convert.ToInt32(control.Value), null);

                if (property.PropertyType == typeof(double))
                    property.SetValue(currentObject, Convert.ToDouble(control.Value), null);

                if (property.PropertyType == typeof(decimal))
                    property.SetValue(currentObject, Convert.ToDecimal(control.Value), null);
            };
            control.Size = new Size(-1, -1);
           
            controlCollection.Add(property.Name, control);

            return control;
        }

        DateTimePicker DateTimePropertyEditor(PropertyInfo property)
        {
            var control = new DateTimePicker
            {
                Value = (DateTime?)property.GetValue(currentObject, null),
                Mode = DateTimePickerMode.DateTime,
                MinDate = DateTime.Today
            };
            control.ValueChanged += (sender, e) =>
            {
                property.SetValue(currentObject, control.Value, null);
            };
           
            controlCollection.Add(property.Name, control);
            return control;
        }

        DateTimePicker TimeSpanPropertyEditor(PropertyInfo property)
        {
            TimeSpan? value = (TimeSpan?)property.GetValue(currentObject, null);

            var control = new DateTimePicker
            {
                Value = value.HasValue ? (DateTime?)new DateTime().Add(value.Value) : null,
                Mode = DateTimePickerMode.Time
            };
            control.ValueChanged += (sender, e) =>
            {
                var newValue = control.Value.HasValue ? (TimeSpan?)control.Value.Value.TimeOfDay : null;
                property.SetValue(currentObject, newValue, null);
            };
          
            controlCollection.Add(property.Name, control);
            return control;
        }

        void OpenReference(ComboBox control)
        {
            var current = control.SelectedValue as ListItem;
            if (current != null)
                (current.Tag as IStorable).ShowDetailContentEmbedded();
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