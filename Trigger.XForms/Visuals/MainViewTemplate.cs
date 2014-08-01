using System;
using System.Collections.Generic;
using Eto.Drawing;
using Eto.Forms;
using Trigger.XStorable.DataStore;
using Trigger.XForms.Controllers;
using System.ComponentModel;

namespace Trigger.XForms.Visuals
{
    public class MainViewTemplate : TemplateBase
    {
        protected Panel ListViewPanel;

        protected Panel NavigationPanel;

        public Type CurrentActiveType
        { 
            get;
            set;
        }

        public MainViewTemplate() : base(typeof(IStorable), null)
        {
            this.WindowState = WindowState.Maximized;

            CreateMainContent();
        }

        void CreateMainContent()
        {
            CreateSplitPanels();

            Content = CreateSplitLayout();
        }

        void CreateSplitPanels()
        {
            NavigationPanel = new Panel();
            NavigationPanel.Content = GetNavigationPanelContent();
            ListViewPanel = new Panel();
        }

        Splitter CreateSplitLayout()
        {
            var splitter = new Splitter()
            {
                Panel1 = NavigationPanel,
                Panel2 = ListViewPanel,
                Orientation = SplitterOrientation.Horizontal,
                FixedPanel = SplitterFixedPanel.Panel1
            };
            splitter.Panel1.Size = new Size(200, -1);
            splitter.Panel2.Size = new Size(-1, -1);
            return splitter;
        }

        DynamicLayout GetNavigationPanelContent()
        {
            var layout = new DynamicLayout();

            foreach (var modelType in ModelTypesDeclarator.DeclaredModelTypes)
            {
                var displayNameAttribute = modelType.FindAttribute<DisplayNameAttribute>();

                var button = new Button()
                {
                    Size = new Size(-1, 40),
                    Text = displayNameAttribute != null ? displayNameAttribute.DisplayName : modelType.Name,
                    Tag = modelType,
                    ImagePosition = ButtonImagePosition.Left
                };

                button.Click += (sender, e) =>
                {
                    CurrentActiveType = button.Tag as Type;
                    var listLayout = new DynamicLayout();
                    listLayout.Add(CreateListViewLayout());
                    ListViewPanel.Content = listLayout;
                };

                layout.BeginHorizontal();
                layout.Add(button, true);
                layout.EndHorizontal();
            }

            layout.BeginHorizontal();
            layout.EndHorizontal();

            return layout;
        }

        DynamicLayout CreateListViewLayout()
        {
            var groupBox = new GroupBox();
            var layout = new DynamicLayout();
            layout.BeginVertical();
            var descriptorType = ListViewDescriptorProvider.GetDescriptor(CurrentActiveType);
            if (descriptorType != null)
            {
                var descriptor = Activator.CreateInstance(descriptorType) as IListViewDescriptor;
                var content = new ListViewBuilder(descriptor, CurrentActiveType).GetContent();
                layout.Add(content);
            }
            layout.EndVertical();
            groupBox.Content = layout;
            return layout;
        }

        public override void OnKeyDown(KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control & e.Key == Keys.Q)
            {
                var result = MessageBox.Show("Close application?", MessageBoxButtons.YesNo, MessageBoxType.Question, MessageBoxDefaultButton.No);
                if (result == DialogResult.Yes)
                    Application.Instance.Quit();
            }
            else
            {
                e.Handled = true;
            }
        }
    }
}
