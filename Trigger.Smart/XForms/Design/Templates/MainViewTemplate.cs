using System;
using System.ComponentModel;
using System.Linq;
using Eto.Drawing;
using Eto.Forms;
using XForms.Commands;
using XForms.Dependency;
using XForms.Store;

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
            this.Size = new Size(1280, 800);
            this.WindowState = WindowState.Maximized;
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
                        Size = new Size(-1, 34),
                        Text = navItem.NavigationItemText,
                        Tag = navItem,
                        Image = ImageExtensions.GetImage(navItem.ImageName, 16),
                        ImagePosition = ButtonImagePosition.Left,                      
                    };
                    button.Click += (sender, e) => ShowListViewFromNavigation(button.Tag as NavigationItem);
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

            foreach (var command in descriptor.Commands)
            {
                var button = new Button()
                {
                    Size = new Size(-1, 34),
                    Text = command.Name,
                    Tag = command,
                    Image = ImageExtensions.GetImage(command.ImageName, 16),
                    ImagePosition = ButtonImagePosition.Left,                      
                };

                button.Click += (sender, e) => (button.Tag as IMainViewCommand).Execute(this);

                appGroupLayout.Add(button, true);

            }
                
            appGroupLayout.EndVertical();

            appGroup.Content = appGroupLayout;

            navigationlayout.Add(appGroup);
          
            navigationlayout.BeginVertical();
            navigationlayout.EndVertical();

            return navigationlayout;
        }

        void ShowListViewFromNavigation(NavigationItem item)
        {
            CurrentActiveType = item.ModelType;

            var currentDisplayNameAttribute = CurrentActiveType.FindAttribute<DisplayNameAttribute>();

            var listLayout = new DynamicLayout();

            listLayout.Add(CreateListViewLayout(item.ListView));

            ContentPanel.Content = listLayout;

            TemplateNavigator.Add(ContentPanel.Content);

            Title = currentDisplayNameAttribute != null ? CurrentActiveType.FindAttribute<DisplayNameAttribute>().DisplayName : CurrentActiveType.Name;
        }

        Control CreateListViewLayout(IListViewDescriptor descriptor)
        {
            Control content = null;

            var layout = new DynamicLayout();
               
            if (descriptor.ListDetailView)
            {
                if (descriptor.ListDetailViewOrientation == ViewItemOrientation.Horizontal)
                    layout.BeginHorizontal();
                else
                    layout.BeginVertical();

                var builder = new ListDetailViewBuilder(descriptor, CurrentActiveType);
                content = builder.GetContent();
            }
            else
            {
                layout.BeginVertical();
                var builder = new ListViewBuilder(descriptor, CurrentActiveType);
                content = builder.GetContent();

                builder.CurrentGridView.MouseDoubleClick += (sender, e) =>
                {
                    var detailContent = CreateDetailViewLayout(builder.CurrentGridView, builder.ModelType);
                    if (detailContent != null)
                    {
                        ContentPanel.Content = detailContent;
                        TemplateNavigator.Add(ContentPanel.Content);
                    }
                };

                builder.CurrentGridView.KeyUp += (sender, e) =>
                {
                    if (e.Key == Keys.Enter)
                    {
                        var detailContent = CreateDetailViewLayout(builder.CurrentGridView, builder.ModelType);
                        if (detailContent != null)
                        {
                            ContentPanel.Content = detailContent;
                            TemplateNavigator.Add(ContentPanel.Content);
                        }
                    }
                };

                layout.EndVertical();
            }
                
            layout.Add(content);

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
    }
}
