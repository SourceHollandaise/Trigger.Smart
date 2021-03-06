using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Eto.Drawing;
using Eto.Forms;
using XForms.Store;

namespace XForms.Design
{
    public class DetailViewBuilder : IViewBuilder
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
            //detailViewLayout.Size = new Size(-1, -1);

            var groupItems = descriptor.GroupItemDescriptions.Where(p => p.Visible).OrderBy(p => p.Index).ToList();

            detailViewLayout.BeginHorizontal();
            if (showCommandBar)
            {
                var commandBarBuilder = new DetailViewCommandBarBuilder(currentObject, descriptor.Commands.Where(p => p.Visible), descriptor.IsTaggable, false);

                detailViewLayout.Add(commandBarBuilder.GetContent());
            }
                
            detailViewLayout.EndHorizontal();
  
            detailViewLayout.BeginHorizontal();

            detailViewLayout.Add(AddGroupLayouts(groupItems));
   
            return detailViewLayout;
        }

        Control CreateTabbedViewLayout()
        {
            var detailViewLayout = new DynamicLayout();
            //detailViewLayout.Size = new Size(-1, -1);

            detailViewLayout.BeginHorizontal();

            if (showCommandBar)
            {
                var commandBarBuilder = new DetailViewCommandBarBuilder(currentObject, descriptor.Commands.Where(p => p.Visible), descriptor.IsTaggable, false);

                detailViewLayout.Add(commandBarBuilder.GetContent());
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

                tabControl.Pages.Add(tabPage);

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
                groupBox.BackgroundColor = layout.BackgroundColor;
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
                //label.Font = new Font(label.Font.Family, label.Font.Size - 2f);

                switch (viewItem.LabelOrientation)
                {
                    case LabelOrientation.Left:
                        if (groupItem.ViewItemOrientation != ViewItemOrientation.Horizontal)
                            layout.BeginHorizontal();
                        if (viewItem.ShowLabel)
                            layout.Add(label);
                        else
                            layout.Add(new Label());
                        layout.Add(control, !viewItem.Fill, !viewItem.Fill);
                        if (groupItem.ViewItemOrientation != ViewItemOrientation.Horizontal)
                            layout.EndHorizontal();
                        break;
                    
                    case LabelOrientation.Right:
                        layout.BeginHorizontal();
                        layout.Add(control, !viewItem.Fill, !viewItem.Fill);
                        if (viewItem.ShowLabel)
                            layout.Add(label);
                        else
                            layout.Add(new Label());
                        layout.EndHorizontal();
                        break;
                    
                    case LabelOrientation.Top:
                        layout.BeginVertical();
                        if (viewItem.ShowLabel)
                            layout.Add(label);
                        else
                            layout.Add(new Label());
                        layout.Add(control, !viewItem.Fill, !viewItem.Fill);
                        layout.EndVertical();
                        break;
                    
                    case LabelOrientation.Bottom:
                        layout.Add(control, !viewItem.Fill, !viewItem.Fill);
                        if (viewItem.ShowLabel)
                            layout.Add(label);
                        else
                            layout.Add(new Label());
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
                try
                {
                    if (control.GetType().GetProperty("ReadOnly") != null)
                        control.GetType().GetProperty("ReadOnly").SetValue(control, viewItem.ReadOnly, null);
                    else
                        control.Enabled = !viewItem.ReadOnly;
                }
                catch
                {

                }
            }
        }
    }
}