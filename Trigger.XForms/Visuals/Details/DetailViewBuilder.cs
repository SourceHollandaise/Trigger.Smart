using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Eto.Drawing;
using Eto.Forms;
using Trigger.XStorable.DataStore;
using Trigger.XForms;
using Trigger.XForms.Commands;
using Trigger.XStorable.Dependency;
using Trigger.BCL.Common.Model;

namespace Trigger.XForms.Visuals
{
    public class DetailViewBuilder
    {
        IList<Button> tagButtons = new List<Button>();

        readonly DetailViewControlFactory factory;

        protected IDetailViewDescriptor Descriptor { get; set; }

        protected IStorable CurrentObject{ get; set; }

        public Type CurrentType{ get; set; }

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

        Control CreateViewLayout()
        {
            var groupItems = Descriptor.GroupItemDescriptions.OrderBy(p => p.Index).ToList();

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
            foreach (var command in Descriptor.Commands)
            {
                var button = new Button();
                button.Size = new Size(40, 40);
                button.ID = command.ID;
                button.ToolTip = command.Name;
                button.Image = ImageExtensions.GetImage(command.ImageName, 24);
                button.ImagePosition = ButtonImagePosition.Overlay;
                button.Click += (sender, e) =>
                {
                    command.Execute(new DetailViewArguments{ CurrentObject = CurrentObject });
                };
                commandBar.Add(button, false, false);

            }
            commandBar.Add(new DynamicLayout(){ Size = new Size(-1, -1) });

            if (Descriptor.IsTaggable)
                AddTagButtonsToCommandBar(commandBar);

            commandBar.EndHorizontal();

            detailViewLayout.Add(commandBar);

            detailViewLayout.EndHorizontal();
            detailViewLayout.BeginHorizontal();

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
                    Size = new Size(-1, -1),
                    Content = AddGroupLayouts(tabItem.GroupItemDescriptions),
                };
   
                tabPage.Content = scrollable;
            }

            detailViewLayout.Add(tabControl);
            detailViewLayout.EndHorizontal();
            return detailViewLayout;
        }

        void AddTagButtonsToCommandBar(DynamicLayout commandBar)
        {
            commandBar.Add(new DynamicLayout(){ Size = new Size(40, -1) });

            commandBar.Add(TagButton(Colors.OrangeRed), false, false);
            commandBar.Add(TagButton(Colors.Orange), false, false);
            commandBar.Add(TagButton(Colors.YellowGreen), false, false);
            commandBar.Add(TagButton(Colors.LightSkyBlue), false, false);
            commandBar.Add(TagButton(Colors.WhiteSmoke), false, false);

            commandBar.Add(new DynamicLayout(){ Size = new Size(-1, -1) });

            if (CurrentObject != null && CurrentObject.MappingId != null)
            {
                var store = DependencyMapProvider.Instance.ResolveType<IStore>();
                var tag = store.LoadAll<Tag>().FirstOrDefault(p => p.TargetObjectMappingId.Equals(CurrentObject.MappingId.ToString()));
                if (tag != null)
                {
                    var tagbutton = tagButtons.FirstOrDefault(p => p.BackgroundColor.Equals(Color.Parse(tag.TagColor)));
                    if (tagbutton != null)
                    {
                        if (Color.Parse(tag.TagColor) == Colors.WhiteSmoke)
                            return;
                        tagbutton.Image = ImageExtensions.GetImage("Accept24", 24);
                        tagbutton.ImagePosition = ButtonImagePosition.Overlay;
                    }
                }
            }
        }

        Button TagButton(Color color)
        {
            var tagbutton = new Button
            {
                Size = new Size(40, 40),
                ID = "tag_" + color,
                BackgroundColor = color,
                ToolTip = "Add Tag "
            };

            tagButtons.Add(tagbutton);

            tagbutton.Click += (sender, e) =>
            {
                CurrentObject.Save();
                var template = CurrentObject.TryGetDetailView();
                if (template != null)
                {
                    foreach (var button in tagButtons)
                        button.Image = null;

                    var store = DependencyMapProvider.Instance.ResolveType<IStore>();
                    var tag = store.LoadAll<Tag>().FirstOrDefault(p => p.TargetObjectMappingId.Equals(CurrentObject.MappingId.ToString()));
                    if (tag == null)
                        tag = new Tag();

                    tag.TargetObjectMappingId = CurrentObject.MappingId.ToString();
                    tag.TagColor = tagbutton.BackgroundColor.ToString();

                    tagbutton.Image = ImageExtensions.GetImage("Accept24", 24);
                    tagbutton.ImagePosition = ButtonImagePosition.Overlay;

                    tag.Save();
                }
            };
                
            return tagbutton;
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
            {
                groupBox.Text = group.GroupHeaderText;
                groupBox.Font = new Font(groupBox.Font.Family, groupBox.Font.Size, FontStyle.Bold);
            }

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
    }
}