using System;
using Eto.Drawing;
using Eto.Forms;
using Trigger.XStorable.DataStore;
using System.ComponentModel;
using Trigger.XStorable.Dependency;
using Trigger.XForms.Commands;
using System.Linq;

namespace Trigger.XForms.Visuals
{
    public sealed class MainViewTemplate : TemplateBase
    {
        Panel listViewPanel;

        Panel navigationPanel;

        public Type CurrentActiveType
        { 
            get;
            set;
        }

        public MainViewTemplate() : base(typeof(IStorable), null)
        {
            this.Size = new Size(1200, 800);
            CreateMainContent();
        }

        void CreateMainContent()
        {
            CreateSplitPanels();

            Content = CreateSplitLayout();
        }

        void CreateSplitPanels()
        {
            navigationPanel = new Panel();
            navigationPanel.Content = GetMainPanelContent();
            listViewPanel = new Panel();
        }

        Splitter CreateSplitLayout()
        {
            var splitter = new Splitter
            {
                Panel1 = navigationPanel,
                Panel2 = listViewPanel,
                Orientation = SplitterOrientation.Horizontal,
                FixedPanel = SplitterFixedPanel.Panel1
            };
            splitter.Panel1.Size = new Size(200, -1);
            splitter.Panel2.Size = new Size(-1, -1);
            return splitter;
        }

        DynamicLayout GetMainPanelContent()
        {
            var descriptor = DependencyMapProvider.Instance.ResolveType<IMainViewDescriptor>();

            var navigationlayout = new DynamicLayout();

            foreach (var groupItem in descriptor.NavigationGroups.OrderBy(p => p.Index).ToList())
            {
                var navGroupLayout = new DynamicLayout();
                var navGroupBox = new GroupBox();
                navGroupBox.Text = groupItem.NavigationGroupText;
                navGroupBox.Font = new Font(navGroupBox.Font.Family, navGroupBox.Font.Size, FontStyle.Bold);
                foreach (var navItem in groupItem.NavigationItems.OrderBy(p => p.Index).ToList())
                {
                    var button = new Button()
                    {
                        Size = new Size(-1, 40),
                        Text = navItem.NavigationItemText,
                        Tag = navItem.ModelType,
                        Image = ImageExtensions.GetImage(navItem.ImageName, 24),
                        ImagePosition = ButtonImagePosition.Left
                    };
                    button.Click += (sender, e) => ShowListViewFromNavigation(button.Tag as Type);
                    navGroupLayout.Add(button, true);
                }
                navGroupBox.Content = navGroupLayout;
                navigationlayout.BeginVertical();
                navigationlayout.Add(navGroupBox);
                navigationlayout.EndHorizontal();
            }

            var appGroupLayout = new DynamicLayout();

            var appGroup = new GroupBox();
            appGroup.Text = "Application";
            appGroup.Font = new Font(appGroup.Font.Family, appGroup.Font.Size, FontStyle.Bold);
            appGroupLayout.BeginVertical();
            appGroupLayout.Add(GetLogOffButton(), true);
            appGroupLayout.Add(GetExitButton(), true);
            appGroupLayout.EndVertical();

            appGroup.Content = appGroupLayout;

            navigationlayout.Add(appGroup);
          
            navigationlayout.BeginVertical();
            navigationlayout.EndVertical();

            return navigationlayout;
        }

        void ShowListViewFromNavigation(Type type)
        {
            CurrentActiveType = type;
            var currentDisplayNameAttribute = CurrentActiveType.FindAttribute<DisplayNameAttribute>();
            var listLayout = new DynamicLayout();
            listLayout.Add(CreateListViewLayout());
            listViewPanel.Content = listLayout;
            Title = currentDisplayNameAttribute != null ? CurrentActiveType.FindAttribute<DisplayNameAttribute>().DisplayName : CurrentActiveType.Name;
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

        Button GetLogOffButton()
        {
            var logOffCommand = DependencyMapProvider.Instance.ResolveType<ILogOffCommand>();
            var logOffButton = new Button
            {
                Size = new Size(-1, 40),
                Image = ImageExtensions.GetImage(logOffCommand.ImageName, 24),
                ImagePosition = ButtonImagePosition.Left,
                Text = logOffCommand.Name,
                ID = logOffCommand.ID
            };
            logOffButton.Click += (sender, e) => logOffCommand.Execute(this);
            return logOffButton;
        }

        Button GetExitButton()
        {
            var exitCommand = DependencyMapProvider.Instance.ResolveType<IApplicationExitCommand>();
            var exitButton = new Button
            {
                Size = new Size(-1, 40),
                Image = ImageExtensions.GetImage(exitCommand.ImageName, 24),
                ImagePosition = ButtonImagePosition.Left,
                Text = exitCommand.Name,
                ID = exitCommand.ID
            };
            exitButton.Click += (sender, e) => exitCommand.Execute(this);
            return exitButton;
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
