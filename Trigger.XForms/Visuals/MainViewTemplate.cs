using System;
using Eto.Drawing;
using Eto.Forms;
using Trigger.XStorable.DataStore;
using Trigger.XForms.Controllers;
using System.ComponentModel;
using Trigger.XStorable.Dependency;
using Trigger.XForms.Commands;

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
            this.Size = new Size(1000, 600);
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
            var navGroupLayout = new DynamicLayout();
            var navGroup = new GroupBox();
            navGroup.Text = "Favorites";
            navGroup.Font = new Font(navGroup.Font.Family, navGroup.Font.Size, FontStyle.Bold);

            foreach (var modelType in ModelTypesDeclarator.DeclaredModelTypes)
            {
                var displayNameAttribute = modelType.FindAttribute<DisplayNameAttribute>();
                var imageNameAttribute = modelType.FindAttribute<ImageNameAttribute>();

                var button = new Button()
                {
                    Size = new Size(-1, 40),
                    Text = displayNameAttribute != null ? displayNameAttribute.DisplayName : modelType.Name,
                    Tag = modelType,
                    Image = imageNameAttribute != null ? ImageExtensions.GetImage(imageNameAttribute.ImageName + ".png", 16) : ImageExtensions.GetImage("info.png", 16),
                    ImagePosition = ButtonImagePosition.Left
                };

                button.Click += (sender, e) =>
                {
                    CurrentActiveType = button.Tag as Type;
                    var currentDisplayNameAttribute = CurrentActiveType.FindAttribute<DisplayNameAttribute>();
                    var listLayout = new DynamicLayout();
                    listLayout.Add(CreateListViewLayout());
                    ListViewPanel.Content = listLayout;
                    Title = currentDisplayNameAttribute != null ? CurrentActiveType.FindAttribute<DisplayNameAttribute>().DisplayName : CurrentActiveType.Name;
                };

                navGroupLayout.BeginVertical();
                navGroupLayout.Add(button, true);
                navGroupLayout.EndVertical();
            }
               
            navGroup.Content = navGroupLayout;

            var appGroup = new GroupBox();
            appGroup.Text = "Application";
            appGroup.Font = new Font(appGroup.Font.Family, appGroup.Font.Size, FontStyle.Bold);

            var logOffCommand = DependencyMapProvider.Instance.ResolveType<ILogOffCommand>();
            var logOffButton = new Button()
            {
                Size = new Size(-1, 40),
                Image = ImageExtensions.GetImage(logOffCommand.ImageName + ".png", 16),
                ImagePosition = ButtonImagePosition.Left,
                Text = logOffCommand.Name,
                ID = logOffCommand.ID
            };
            logOffButton.Click += (sender, e) =>
            {
                logOffCommand.Execute(this);
            };

            var exitCommand = DependencyMapProvider.Instance.ResolveType<IApplicationExitCommand>();
            var exitButton = new Button()
            {
                Size = new Size(-1, 40),
                Image = ImageExtensions.GetImage(exitCommand.ImageName + ".png", 16),
                ImagePosition = ButtonImagePosition.Left,
                Text = exitCommand.Name,
                ID = exitCommand.ID
            };
            exitButton.Click += (sender, e) =>
            {
                exitCommand.Execute(this);
            };


            var appGroupLayout = new DynamicLayout();
            appGroupLayout.BeginVertical();

            appGroupLayout.Add(logOffButton, true);
            appGroupLayout.Add(exitButton, true);
            appGroupLayout.EndVertical();

            appGroup.Content = appGroupLayout;

            var sidePaneLayout = new DynamicLayout();

            sidePaneLayout.Add(navGroup);
            sidePaneLayout.Add(appGroup);
            sidePaneLayout.BeginVertical();
            sidePaneLayout.EndVertical();

            return sidePaneLayout;
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
