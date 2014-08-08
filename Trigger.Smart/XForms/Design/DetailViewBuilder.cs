using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Eto.Drawing;
using Eto.Forms;
using XForms.Store;
using XForms.Commands;

namespace XForms.Design
{
    public class DetailViewBuilder
    {
        readonly DetailViewControlFactory factory;

        readonly IDetailViewDescriptor descriptor;

        readonly IStorable currentObject;

        readonly Type currentType;

        bool showCommandBar;

        public DetailViewBuilder(IDetailViewDescriptor descriptor, IStorable currentObject, bool showCommandBar = true)
        {
            this.descriptor = descriptor;
            this.currentObject = currentObject;
            this.currentType = this.currentObject.GetType();
            this.showCommandBar = showCommandBar;
            factory = new DetailViewControlFactory(this.currentObject);
        }

        public Control GetContent()
        {
            Control content = null;
            if (descriptor.TabItemDescriptions != null && descriptor.TabItemDescriptions.Any())
                content = CreateTabbedViewLayout();
            else
                content = CreateViewLayout();

            RegisterForAutoSave();

            return content;
        }

        void RegisterForAutoSave()
        {
            if (descriptor.AutoSave)
            {
                currentObject.PropertyChanged += (sender, e) =>
                {
                    currentObject.Save();
                };
            }
        }

        Control CreateViewLayout()
        {
            var detailViewLayout = new DynamicLayout();
            detailViewLayout.BeginHorizontal();
            var groupItems = descriptor.GroupItemDescriptions.Where(p => p.Visible).OrderBy(p => p.Index).ToList();

            if (showCommandBar)
            {
                var commandBarLayout = CreateCommandBar();
                detailViewLayout.Add(commandBarLayout);
            }

            detailViewLayout.EndHorizontal();
            detailViewLayout.BeginHorizontal();

            detailViewLayout.Add(AddGroupLayouts(groupItems));
            //detailViewLayout.EndHorizontal();

            return detailViewLayout;
 
        }

        Control CreateTabbedViewLayout()
        {
            var detailViewLayout = new DynamicLayout();
            detailViewLayout.BeginHorizontal();

            if (showCommandBar)
            {
                var commandBarLayout = CreateCommandBar();
                detailViewLayout.Add(commandBarLayout);
            }

            detailViewLayout.EndHorizontal();
            detailViewLayout.BeginHorizontal();

            var tabItems = descriptor.TabItemDescriptions.Where(p => p.Visible).OrderBy(p => p.Index).ToList();

            var tabControl = new TabControl();
           
            foreach (var tabItem in tabItems)
            { 
                var tabPage = new TabPage()
                {
                    Text = tabItem.TabHeaderText,
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

        GroupBox CreateGroupLayout(GroupItemDescription groupItem)
        {
            var layout = new DynamicLayout();

            var groupBox = new GroupBox();
            groupBox.BackgroundColor = Colors.WhiteSmoke;
            if (!string.IsNullOrEmpty(groupItem.GroupHeaderText))
            {
                groupBox.Text = groupItem.GroupHeaderText;

                try
                {
                    groupBox.Font = new Font(groupBox.Font.Family, groupBox.Font.Size, FontStyle.Bold);
                }
                catch
                {

                }
            }

            if (groupItem.ViewItemOrientation == ViewItemOrientation.Horizontal)
                layout.BeginHorizontal();

            foreach (var viewItem in groupItem.ViewItemDescriptions.Where(p => p.Visible).OrderBy(p => p.Index).ToList())
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
                        if (groupItem.ViewItemOrientation != ViewItemOrientation.Horizontal)
                            layout.BeginHorizontal();
                        if (viewItem.ShowLabel)
                            layout.Add(label);
                        layout.Add(control, !viewItem.Fill, !viewItem.Fill);
                        if (groupItem.ViewItemOrientation != ViewItemOrientation.Horizontal)
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

            if (groupItem.ViewItemOrientation == ViewItemOrientation.Horizontal)
                layout.EndHorizontal();

            if (!groupItem.Fill)
            {
                layout.BeginVertical();
                layout.EndVertical();
            }

            groupBox.Content = layout;

            return groupBox;
        }

        void DecorateControlsWithDescriptionValues(ViewItemDescription viewItem, Control control)
        {
            if (control != null && viewItem.ReadOnly)
            {
                if (control.GetType().GetProperty("ReadOnly") != null)
                    control.GetType().GetProperty("ReadOnly").SetValue(control, viewItem.ReadOnly, null);
                else
                    control.Enabled = !viewItem.ReadOnly;
            }
        }

        DynamicLayout CreateCommandBar()
        {
            var commandBarLayout = new DynamicLayout();
            commandBarLayout.BeginHorizontal();
            foreach (var command in descriptor.Commands.Where(p => p.Visible).ToList())
            {
                var button = new Button();
                button.Size = new Size(command.Width, 34);
                button.ID = command.ID;
                button.ToolTip = command.Name;
                button.Text = command.Name;
                button.Click += (sender, e) =>
                {
                    command.Execute(new DetailViewArguments
                    {
                        CurrentObject = currentObject
                    });
                };
                commandBarLayout.Add(button, false, false);
            }
            commandBarLayout.Add(new DynamicLayout()
            {
                Size = new Size(-1, -1)
            });
            if (descriptor.IsTaggable)
                new TagButtonBuilder(currentObject).AddTagButtonsContent(commandBarLayout);
            commandBarLayout.EndHorizontal();
            return commandBarLayout;
        }
    }
}