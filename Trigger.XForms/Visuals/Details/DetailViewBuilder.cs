using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Eto.Drawing;
using Eto.Forms;
using Trigger.XStorable.DataStore;
using Trigger.XForms;
using Trigger.XForms.Commands;

namespace Trigger.XForms.Visuals
{
    public class DetailViewBuilder
    {
        readonly DetailViewControlFactory factory;

        readonly IDetailViewDescriptor descriptor;

        readonly IStorable currentObject;

        readonly Type currentType;

        public DetailViewBuilder(IDetailViewDescriptor descriptor, IStorable currentObject)
        {
            this.descriptor = descriptor;
            this.currentObject = currentObject;
            this.currentType = this.currentObject.GetType();

            factory = new DetailViewControlFactory(this.currentObject);
        }

        public Control GetContent()
        {
            if (descriptor.TabItemDescriptions != null && descriptor.TabItemDescriptions.Any())
                return CreateTabbedViewLayout();

            return CreateViewLayout();
        }

        Control CreateViewLayout()
        {
            var groupItems = descriptor.GroupItemDescriptions.Where(p => p.Visible).OrderBy(p => p.Index).ToList();

            return new Scrollable()
            {
                Content = AddGroupLayouts(groupItems)
            };
        }

        Control CreateTabbedViewLayout()
        {
            var detailViewLayout = new DynamicLayout();
            detailViewLayout.BeginHorizontal();

            var commandBar = new DynamicLayout();
            commandBar.BeginHorizontal();
            foreach (var command in descriptor.Commands.Where(p => p.Visible).ToList())
            {
                var button = new Button();
                button.Size = new Size(40, 40);
                button.ID = command.ID;
                button.ToolTip = command.Name;
                button.Image = ImageExtensions.GetImage(command.ImageName, 24);
                button.ImagePosition = ButtonImagePosition.Overlay;
                //button.BackgroundColor = Colors.WhiteSmoke;
                button.Click += (sender, e) =>
                {
                    command.Execute(new DetailViewArguments{ CurrentObject = currentObject });
                };
                commandBar.Add(button, false, false);

            }
            commandBar.Add(new DynamicLayout(){ Size = new Size(-1, -1) });

            if (descriptor.IsTaggable)
                new TagButtonBuilder(currentObject).AddTagButtonsContent(commandBar);

            commandBar.EndHorizontal();

            detailViewLayout.Add(commandBar);

            detailViewLayout.EndHorizontal();
            detailViewLayout.BeginHorizontal();

            var tabItems = descriptor.TabItemDescriptions.Where(p => p.Visible).OrderBy(p => p.Index).ToList();

            var tabControl = new TabControl();
           
            foreach (var tabItem in tabItems)
            { 
                var tabPage = new TabPage()
                {
                    Text = tabItem.TabHeaderText,
                    //BackgroundColor = Colors.WhiteSmoke
                };

                tabControl.TabPages.Add(tabPage);
                /*
                var scrollable = new Scrollable()
                {
                    Border = BorderType.None,
                    Size = new Size(-1, -1),
                    Content = AddGroupLayouts(tabItem.GroupItemDescriptions),
                };
                */
                tabPage.Content = AddGroupLayouts(tabItem.GroupItemDescriptions.Where(p => p.Visible).OrderBy(p => p.Index).ToList());//scrollable;
            }

            detailViewLayout.Add(tabControl);
            detailViewLayout.EndHorizontal();
            return detailViewLayout;
        }

        DynamicLayout AddGroupLayouts(IEnumerable<GroupItemDescription> groupItems)
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
            groupBox.BackgroundColor = Colors.WhiteSmoke;
            if (!string.IsNullOrEmpty(group.GroupHeaderText))
            {
                groupBox.Text = group.GroupHeaderText;
                groupBox.Font = new Font(groupBox.Font.Family, groupBox.Font.Size, FontStyle.Bold);
            }

            if (group.ViewItemOrientation == ViewItemOrientation.Horizontal)
                layout.BeginHorizontal();

            foreach (var viewItem in group.ViewItemDescriptions.Where(p => p.Visible).OrderBy(p => p.Index).ToList())
            {
                Control control = null;

                if (!viewItem.FieldName.Equals("EmptySpace"))
                {
                    var property = currentType.GetProperty(viewItem.FieldName);

                    if (property == null)
                        continue;

                    control = factory.GetControl(property, viewItem);

                    DecorateControlsWithDescriptionValues(viewItem, control);
                }
                else
                {
                    control = new DynamicLayout();
                }

                if (control == null)
                    continue;

                if (viewItem.Fill)
                    control.Size = new Size(-1, -1);

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

        void DecorateControlsWithDescriptionValues(ViewItemDescription viewItem, Control control)
        {
            if (viewItem.ReadOnly)
            {
                if (control.GetType().GetProperty("ReadOnly") != null)
                    control.GetType().GetProperty("ReadOnly").SetValue(control, viewItem.ReadOnly, null);
                else
                    control.Enabled = !viewItem.ReadOnly;
            }
        }
    }
}