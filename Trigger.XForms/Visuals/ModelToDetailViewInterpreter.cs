using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Eto.Forms;
using Trigger.XStorable.DataStore;
using Trigger.XForms;

namespace Trigger.XForms.Visuals
{
    public class ModelToDetailViewInterpreter
    {
        readonly DetailViewControlFactory controlFactory;

        protected ViewDescriptor Descriptor
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

        public ModelToDetailViewInterpreter(ViewDescriptor descriptor, IStorable currentObject)
        {
            this.Descriptor = descriptor;
            this.CurrentObject = currentObject;
            this.CurrentType = this.CurrentObject.GetType();

            controlFactory = new DetailViewControlFactory(this.CurrentObject);
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

            var scrollable = new Scrollable();

            foreach (var tabItem in tabItems)
            { 
                var tabPage = new TabPage();
                tabPage.Text = tabItem.TabHeaderText;

                tabControl.TabPages.Add(tabPage);
                tabPage.Content = AddGroupLayouts(tabItem.GroupItemDescriptions);
            }

            scrollable.Content = tabControl;

            return scrollable;
        }

        Control CreateViewLayout()
        {
            var groupItems = Descriptor.GroupItemDescriptions.OrderBy(p => p.Index).ToList();

            var scrollable = new Scrollable();

            scrollable.Content = AddGroupLayouts(groupItems);
            return scrollable;
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

            layout.BeginVertical();

            var groupBox = new GroupBox();
            groupBox.Text = group.GroupHeaderText;

            foreach (var viewItem in group.ViewItemDescriptions.OrderBy(p => p.Index).ToList())
            {
                var property = CurrentType.GetProperty(viewItem.FieldName);

                if (property == null)
                    continue;

                var control = controlFactory.GetControl(property);

                if (control == null)
                    continue;

                if (viewItem.Fill)
                    control.Size = new Eto.Drawing.Size(-1, -1);

                var label = new Label{ Text = viewItem.LabelText };

                switch (viewItem.LabelOrientation)
                {
                    case LabelOrientation.Left:
                        layout.BeginHorizontal();
                        if (viewItem.ShowLabel)
                            layout.Add(label);
                        layout.Add(control, !viewItem.Fill, !viewItem.Fill);
                        layout.EndHorizontal();
                        break;
                    case LabelOrientation.Right:
                        layout.BeginHorizontal();
                        layout.Add(control);
                        if (viewItem.ShowLabel)
                            layout.Add(label);
                        layout.EndHorizontal();
                        break;
                    case LabelOrientation.Top:
                        if (viewItem.ShowLabel)
                            layout.Add(label, !viewItem.Fill, !viewItem.Fill);
                        layout.Add(control);
                        break;
                    case LabelOrientation.Bottom:
                        layout.Add(control);
                        if (viewItem.ShowLabel)
                            layout.Add(label);
                        break;
                }
            }

            layout.EndVertical();
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