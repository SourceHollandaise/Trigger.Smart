using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Eto.Forms;
using Trigger.XStorable.DataStore;
using Trigger.XForms;

namespace Trigger.XForms.Visuals
{
    public class DetailViewBuilder
    {
        readonly DetailViewControlFactory factory;

        protected IDetailViewDescriptor Descriptor
        {
            get;
            set;
        }

        protected IStorable CurrentObject
        {
            get;
            set;
        }

        public Type CurrentType
        {
            get;
            set;
        }

        public DetailViewBuilder(IDetailViewDescriptor descriptor, IStorable currentObject)
        {
            this.Descriptor = descriptor;
            this.CurrentObject = currentObject;
            this.CurrentType = this.CurrentObject.GetType();

            factory = new DetailViewControlFactory(this.CurrentObject);
        }

        public Control GetContent()
        {
            if (Descriptor.TabItemDescriptions != null && Descriptor.TabItemDescriptions.Any())
                return CreateTabbedViewLayout();

            return CreateViewLayout();
        }

        Control CreateTabbedViewLayout()
        {
            var tabItems = Descriptor.TabItemDescriptions.OrderBy(p => p.Index).ToList();

            var tabControl = new TabControl();

            foreach (var tabItem in tabItems)
            { 
                var tabPage = new TabPage()
                {
                    Text = tabItem.TabHeaderText,
                };

                tabControl.TabPages.Add(tabPage);
           
                var scrollable = new Scrollable()
                {
                    Border = BorderType.None,
                    Size = new Eto.Drawing.Size(-1, -1),
                    Content = AddGroupLayouts(tabItem.GroupItemDescriptions),
                };
               
                tabPage.Content = scrollable;
            }

            return tabControl;
        }

        Control CreateViewLayout()
        {
            var groupItems = Descriptor.GroupItemDescriptions.OrderBy(p => p.Index).ToList();

            return new Scrollable()
            {
                Content = AddGroupLayouts(groupItems)
            };
        }

        DynamicLayout AddGroupLayouts(IList<GroupItemDescription> groupItems)
        {
            var layout = new DynamicLayout();

            foreach (var groupItem in groupItems)
            {
                var groupLayout = CreateGroupLayout(groupItem);
                layout.Add(groupLayout);
                if (!groupItem.Fill)
                {
                    layout.BeginHorizontal();
                    layout.EndHorizontal();
                }
            }
           
            return layout;
        }

        GroupBox CreateGroupLayout(GroupItemDescription group)
        {
            var layout = new DynamicLayout();

            var groupBox = new GroupBox();
            if (!string.IsNullOrEmpty(group.GroupHeaderText))
                groupBox.Text = group.GroupHeaderText;

            if (group.ViewItemOrientation == ViewItemOrientation.Horizontal)
                layout.BeginHorizontal();

            foreach (var viewItem in group.ViewItemDescriptions.OrderBy(p => p.Index).ToList())
            {
                Control control = null;

                if (!viewItem.FieldName.Equals("EmptySpace"))
                {
                    var property = CurrentType.GetProperty(viewItem.FieldName);

                    if (property == null)
                        continue;

                    control = factory.GetControl(property, viewItem);
                }
                else
                {
                    control = new DynamicLayout();
                }

                if (control == null)
                    continue;

                if (viewItem.Fill)
                    control.Size = new Eto.Drawing.Size(-1, -1);

                var label = new Label{ Text = viewItem.LabelText };

                switch (viewItem.LabelOrientation)
                {
                    case LabelOrientation.Left:
                        if (group.ViewItemOrientation != ViewItemOrientation.Horizontal)
                            layout.BeginHorizontal();
                        if (viewItem.ShowLabel)
                            layout.Add(label);
                        layout.Add(control, !viewItem.Fill, !viewItem.Fill);
                        if (group.ViewItemOrientation != ViewItemOrientation.Horizontal)
                            layout.EndHorizontal();
                        break;
                    
                    case LabelOrientation.Right:
                        layout.BeginHorizontal();
                        layout.Add(control, !viewItem.Fill, !viewItem.Fill);
                        if (viewItem.ShowLabel)
                            layout.Add(label);
                        layout.EndHorizontal();
                        break;
                    
                    case LabelOrientation.Top:
                        layout.BeginVertical();
                        if (viewItem.ShowLabel)
                            layout.Add(label);
                        layout.Add(control, !viewItem.Fill, !viewItem.Fill);
                        layout.EndVertical();
                        break;
                    
                    case LabelOrientation.Bottom:
                        layout.Add(control, !viewItem.Fill, !viewItem.Fill);
                        if (viewItem.ShowLabel)
                            layout.Add(label);
                        break;
                }
            }

            if (group.ViewItemOrientation == ViewItemOrientation.Horizontal)
                layout.EndHorizontal();

            if (!group.Fill)
            {
                layout.BeginVertical();
                layout.EndVertical();
            }
            groupBox.Content = layout;

            return groupBox;
        }
    }
}