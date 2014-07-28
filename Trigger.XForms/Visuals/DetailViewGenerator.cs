using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Eto.Drawing;
using Eto.Forms;
using Trigger.XForms;
using Trigger.XStorable.DataStore;
using Trigger.XStorable.Dependency;

namespace Trigger.XForms.Visuals
{
    public class DetailViewGenerator
    {
        List<CreatableDetailItem> creatableItems = new List<CreatableDetailItem>();

        protected DetailPropertyEditorFactory EditorFactory
        {
            get;
            set;
        }

        protected IStorable Model
        {
            get;
            set;
        }


        protected IViewTemplateConfiguration ViewTemplateConfig
        {
            get
            {
                return DependencyMapProvider.Instance.ResolveType<IViewTemplateConfiguration>();
            }
        
        }

        public DetailViewGenerator(IStorable model)
        {
            this.Model = model;
            EditorFactory = new DetailPropertyEditorFactory(Model);
        }

        public DynamicLayout GetContent()
        {
            ResolveDetailViewControls();
            return GetDynamicContent();
        }

        void ResolveDetailViewControls()
        {
            var properties = Model.GetType().GetProperties();

            foreach (var property in properties)
            {
                var item = new CreatableDetailItem();

                var visibilityAttribute = property.GetCustomAttributes(typeof(VisibleOnViewAttribute), true).FirstOrDefault() as VisibleOnViewAttribute;

                if (visibilityAttribute != null && (visibilityAttribute.TargetView == TargetView.ListOnly || visibilityAttribute.TargetView == TargetView.None))
                    continue;

                var inGroupAttribute = property.GetCustomAttributes(typeof(InGroupAttribute), true).FirstOrDefault() as InGroupAttribute;
                if (inGroupAttribute != null)
                {
                    item.Group = inGroupAttribute.GroupName;
                    item.GroupIndex = inGroupAttribute.GroupIndex;
                    item.PropertyIndex = inGroupAttribute.PropertyIndex;
                }

                item.Property = property;

                if (property.PropertyType == typeof(string))
                {
                    item.Control = EditorFactory.StringPropertyEditor(property);
                    creatableItems.Add(item);
                }

                if (property.PropertyType == typeof(DateTime?) || property.PropertyType == typeof(DateTime))
                {
                    item.Control = EditorFactory.DateTimePropertyEditor(property);
                    creatableItems.Add(item);
                }

                if (property.PropertyType == typeof(TimeSpan?) || property.PropertyType == typeof(TimeSpan))
                {
                    item.Control = EditorFactory.TimeSpanPropertyEditor(property);
                    creatableItems.Add(item);
                }

                if (property.PropertyType == typeof(bool))
                {
                    item.Control = EditorFactory.BooleanPropertyEditor(property);
                    creatableItems.Add(item);
                }

                if (typeof(IStorable).IsAssignableFrom(property.PropertyType))
                {
                    item.Control = EditorFactory.ReferencePropertyEditor(property);
                    creatableItems.Add(item);
                }

                if (property.PropertyType == typeof(int) || property.PropertyType == typeof(double) || property.PropertyType == typeof(decimal))
                {
                    item.Control = EditorFactory.NumberPropertyEditor(property);
                    creatableItems.Add(item);
                }

                if (property.PropertyType.BaseType == typeof(Enum))
                {
                    item.Control = EditorFactory.EnumPropertyEditor(property);
                    creatableItems.Add(item);
                }

                var linkedListAttribute = item.Property.GetCustomAttributes(typeof(LinkedListAttribute), true).FirstOrDefault() as LinkedListAttribute; 

                if (linkedListAttribute != null)
                {
                    var value = item.Property.GetValue(Model, null);
                    if (value is IEnumerable<IStorable>)
                    {
                        var list = (value as IEnumerable<IStorable>).ToList();
                        if (!list.Any)
                            continue;

                        var openLinkedListButton = new Button
                        {
                            Tag = value
                        };

                        var displayNameAttribute = item.Property.GetCustomAttributes(typeof(DisplayNameAttribute), true).FirstOrDefault() as DisplayNameAttribute;
                        openLinkedListButton.Text = displayNameAttribute != null ? displayNameAttribute.DisplayName : property.PropertyType.Name;

                        openLinkedListButton.Click += (sender, e) =>
                        {
                            var listView = new ListViewTemplate(linkedListAttribute.LinkType, null);
                            listView.ReloadList((IEnumerable<IStorable>)openLinkedListButton.Tag);
                            listView.Show();
                        };

                        item.Control = openLinkedListButton;
                        creatableItems.Add(item);
                    }
                }
            }
        }

        DynamicLayout GetDynamicContent()
        {
            var layout = new DynamicLayout();
            layout.BeginVertical();

            var subGroups = creatableItems.Where(p => !string.IsNullOrWhiteSpace(p.Group)).OrderBy(p => p.GroupIndex)
                .GroupBy(item => item.Group)
                .Select(p => new List<CreatableDetailItem>(p))
                .ToArray();

            foreach (var item in creatableItems.Where(p => string.IsNullOrWhiteSpace(p.Group)))
            {
                layout.BeginHorizontal();
                layout.Add(GetLabel(item.Property), false);
                layout.Add(item.Control, false);
                layout.EndHorizontal();
            }

            foreach (var items in subGroups)
            {
                var group = GetLayoutGroup(items);
                if (group != null)
                    layout.Add(group, false, false);
            }
                
            layout.EndVertical();

            layout.BeginVertical();
            layout.EndVertical();

            return layout;
        }

        GroupBox GetLayoutGroup(IList<CreatableDetailItem> items)
        {
            if (items.Any())
            {
                var layout = new DynamicLayout();
                layout.BeginVertical();

                var groupBox = new GroupBox();

                foreach (var item in items.OrderBy( p => p.PropertyIndex).ToArray())
                {
                    if (string.IsNullOrWhiteSpace(groupBox.Text))
                        groupBox.Text = item.Group;
               
                    if (string.IsNullOrWhiteSpace(groupBox.Text))
                        groupBox.Text = item.Group;

                    if (item.Control is Button && item.Property.PropertyType.IsGenericType)
                    {
                        layout.BeginVertical();
                        //layout.Add(GetLabel(item.Property), false);
                        layout.Add(item.Control, true);
                        layout.EndVertical();

                    }
                    else
                    { 
                        switch (ViewTemplateConfig.LabelLocation)
                        {
                            case LabelControlLocation.AboveControl:
                                layout.BeginVertical();
                                layout.Add(GetLabel(item.Property), false);
                                layout.Add(item.Control, true);
                                layout.EndVertical();
                                break;
                            case LabelControlLocation.BeforeControl:
                                layout.BeginHorizontal();
                                layout.Add(GetLabel(item.Property), false);
                                layout.Add(item.Control, true);
                                layout.EndHorizontal();
                                break;
                            case LabelControlLocation.None:
                                layout.BeginHorizontal();
                                layout.Add(item.Control, true);
                                layout.EndHorizontal();
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                    }
                }
                
                layout.EndHorizontal();

                groupBox.Content = layout;

                return groupBox;
            }
            return null;
        }

        Label GetLabel(MemberInfo property)
        {
            var attribute = property.GetCustomAttributes(typeof(DisplayNameAttribute), true).FirstOrDefault() as DisplayNameAttribute;

            var label = new Label
            {
                Text = (attribute != null ? attribute.DisplayName : property.Name) + ":",
                Size = ViewTemplateConfig.LabelLocation == LabelControlLocation.BeforeControl ? new Size(116, -1) : new Size(-1, -1),
                Wrap = WrapMode.Word
            };

            return label;
        }

        void AddReferenceButtons(DynamicLayout layout, ComboBox referenceComboBox)
        {
            layout.BeginVertical();
            layout.BeginHorizontal();
            layout.Add(EditorFactory.ReferenceOpenButton(referenceComboBox), true);
            layout.Add(EditorFactory.ReferenceClearButton(referenceComboBox), true);
            layout.EndHorizontal();
            layout.EndVertical();
        }
    }
}