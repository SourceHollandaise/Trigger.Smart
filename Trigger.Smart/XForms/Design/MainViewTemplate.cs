using System;
using Eto.Drawing;
using Eto.Forms;
using System.ComponentModel;
using System.Linq;
using XForms.Store;
using XForms.Dependency;
using XForms.Commands;

namespace XForms.Design
{

    public sealed class MainViewTemplate : TemplateBase
    {

        public Panel ContentPanel
        {
            get;
            set;
        }

        public Panel NavigationPanel
        {
            get;
            set;
        }

        public Type CurrentActiveType
        { 
            get;
            set;
        }

        public MainViewTemplate() : base(typeof(IStorable), null)
        {
            this.Size = new Size(1200, 800);
            this.Minimizable = true;
            this.Maximizable = true;
     
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
            NavigationPanel.Content = GetMainPanelContentButtonStyle();
            ContentPanel = new Panel();
        }

        Splitter CreateSplitLayout()
        {
            var splitter = new Splitter
            {
                Panel1 = NavigationPanel,
                Panel2 = ContentPanel,
                Orientation = SplitterOrientation.Horizontal,
                FixedPanel = SplitterFixedPanel.Panel1
            };
            splitter.Panel1.Size = new Size(200, -1);
            splitter.Panel2.Size = new Size(-1, -1);
            return splitter;
        }

        Control GetMainPanelContentButtonStyle()
        {
            var descriptor = MapProvider.Instance.ResolveType<IMainViewDescriptor>();

            var navigationlayout = new DynamicLayout();

            foreach (var groupItem in descriptor.NavigationGroups.Where(p => p.Visible).OrderBy(p => p.Index).ToList())
            {
                var navGroupLayout = new DynamicLayout();
                var navGroupBox = new GroupBox();
                navGroupBox.Text = groupItem.NavigationGroupText;

                try
                {
                    navGroupBox.Font = new Font(navGroupBox.Font.Family, navGroupBox.Font.Size, FontStyle.Bold);
                }
                catch
                {

                }

                foreach (var navItem in groupItem.NavigationItems.Where(p => p.Visible).OrderBy(p => p.Index).ToList())
                {
                    var button = new Button()
                    {
                        Size = new Size(-1, 36),
                        Text = navItem.NavigationItemText,
                        Tag = navItem.ModelType,
                        Image = ImageExtensions.GetImage(navItem.ImageName, 16),
                        ImagePosition = ButtonImagePosition.Left,                      
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

            try
            {
                appGroup.Font = new Font(appGroup.Font.Family, appGroup.Font.Size, FontStyle.Bold);
            }
            catch
            {

            }

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

        Control GetMainPanelContentListBoxStyle()
        {
            var descriptor = MapProvider.Instance.ResolveType<IMainViewDescriptor>();

            var navigationlayout = new DynamicLayout();

            foreach (var groupItem in descriptor.NavigationGroups.Where(p => p.Visible).OrderBy(p => p.Index).ToList())
            {
                var navGroupLayout = new DynamicLayout();
                var navGroupBox = new GroupBox();
                navGroupBox.Text = groupItem.NavigationGroupText;

                var listBox = new ListBox();
                listBox.Size = new Size(-1, -1);

                listBox.MouseDoubleClick += (sender, e) =>
                {
                    ShowListViewFromNavigation((listBox.SelectedValue as ListItem).Tag as Type);
                };

                try
                {
                    navGroupBox.Font = new Font(navGroupBox.Font.Family, navGroupBox.Font.Size, FontStyle.Bold);
                }
                catch
                {

                }

                foreach (var navItem in groupItem.NavigationItems.Where(p => p.Visible).OrderBy(p => p.Index).ToList())
                {
                    var listItem = new ListItem();
                    listItem.Text = navItem.NavigationItemText;
                    listItem.Key = navItem.NavigationItemText;
                    listItem.Tag = navItem.ModelType;
                    listBox.Items.Add(listItem);

                }

                navGroupLayout.Add(listBox, true);

                navGroupBox.Content = navGroupLayout;
                navigationlayout.BeginVertical();
                navigationlayout.Add(navGroupBox);
                navigationlayout.EndHorizontal();
            }

            var appGroupLayout = new DynamicLayout();

            var appGroup = new GroupBox();
            appGroup.Text = "Application";

            try
            {
                appGroup.Font = new Font(appGroup.Font.Family, appGroup.Font.Size, FontStyle.Bold);
            }
            catch
            {

            }

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
            ContentPanel.Content = listLayout;
            TemplateNavigator.Add(ContentPanel.Content);

            Title = currentDisplayNameAttribute != null ? CurrentActiveType.FindAttribute<DisplayNameAttribute>().DisplayName : CurrentActiveType.Name;
        }

        Control CreateListViewLayout()
        {
            Control content = null;
            var groupBox = new GroupBox();
            var layout = new DynamicLayout();
            layout.BeginVertical();
            var descriptorType = ListViewDescriptorProvider.GetDescriptor(CurrentActiveType);
            if (descriptorType != null)
            {
                var descriptor = Activator.CreateInstance(descriptorType) as IListViewDescriptor;
                var builder = new ListViewBuilder(descriptor, CurrentActiveType);
                content = builder.GetContent();

                builder.CurrentGridView.MouseDoubleClick += (sender, e) =>
                {
                    var detailContent = CreateDetailViewLayout(builder.CurrentGridView, builder.ModelType);
                    if (detailContent != null)
                        ContentPanel.Content = detailContent;
                };

                builder.CurrentGridView.KeyUp += (sender, e) =>
                {
                    if (e.Key == Keys.Enter)
                    {
                        var detailContent = CreateDetailViewLayout(builder.CurrentGridView, builder.ModelType);
                        if (detailContent != null)
                            ContentPanel.Content = detailContent;
                    }
                };

                layout.Add(content);
            }
            layout.EndVertical();
            groupBox.Content = layout;
            return layout;
        }

        Control CreateDetailViewLayout(GridView currentGridView, Type modelType)
        {
            var currentObject = currentGridView.SelectedItem as IStorable;
            if (currentObject != null)
            {
                currentObject.ShowDetailContentEmbedded();
            }

            return null;
        }

        Button GetLogOffButton()
        {
            var logOffCommand = MapProvider.Instance.ResolveType<ILogonCommand>();
            var logOffButton = new Button
            {
                Size = new Size(-1, 36),
                Image = ImageExtensions.GetImage(logOffCommand.ImageName, 16),
                ImagePosition = ButtonImagePosition.Left,
                Text = logOffCommand.Name,
                ID = logOffCommand.ID
            };
            logOffButton.Click += (sender, e) => logOffCommand.Execute(this);
            return logOffButton;
        }

        Button GetExitButton()
        {
            var exitCommand = MapProvider.Instance.ResolveType<IApplicationExitCommand>();
            var exitButton = new Button
            {
                Size = new Size(-1, 36),
                Image = ImageExtensions.GetImage(exitCommand.ImageName, 16),
                ImagePosition = ButtonImagePosition.Left,
                Text = exitCommand.Name,
                ID = exitCommand.ID
            };
            exitButton.Click += (sender, e) => exitCommand.Execute(this);
            return exitButton;
        }

        public override void OnKeyDown(KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Application & e.Key == Keys.Q)
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
