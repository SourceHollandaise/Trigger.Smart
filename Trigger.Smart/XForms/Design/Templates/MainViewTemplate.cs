using System;
using System.Linq;
using Eto.Drawing;
using Eto.Forms;
using XForms.Commands;
using XForms.Dependency;
using XForms.Store;

namespace XForms.Design
{

    public sealed class MainViewTemplate : TemplateBase, IMainViewTemplate
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
            this.Size = (Size)Screen.WorkingArea.Size;
            this.WindowState = WindowState.Maximized;
            this.Minimizable = true;
            this.Maximizable = true;

            CreateMainContent();

            CreateMenu();
        }

        public bool IsReduced
        {
            get
            {
                return false;
            }
        }

        public void SetContent(Control control)
        {
            ContentPanel.Content = control;
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
                        Tag = navItem,
                        BackgroundColor = Colors.GreenYellow,
                    };

                    switch (navItem.DisplayStyle)
                    {
                        case ButtonDisplayStyle.Image:
                            button.Image = ImageExtensions.GetImage(navItem.ImageName, 16);
                            button.ImagePosition = ButtonImagePosition.Above;
                            break;
                        case ButtonDisplayStyle.Text:
                            button.Text = navItem.NavigationItemText;
                            break;
                        case ButtonDisplayStyle.ImageAndText:
                            button.Text = navItem.NavigationItemText;
                            button.Image = ImageExtensions.GetImage(navItem.ImageName, 16);
                            button.ImagePosition = ButtonImagePosition.Left;
                            break;
                    }            

                    button.Click += (sender, e) => ShowListViewFromNavigation(button.Tag as NavigationItemDescription);
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
                    Tag = command,
                    BackgroundColor = Colors.GreenYellow
                };

                switch (command.DisplayStyle)
                {
                    case ButtonDisplayStyle.Image:
                        button.Image = ImageExtensions.GetImage(command.ImageName, 16);
                        button.ImagePosition = ButtonImagePosition.Above;
                        break;
                    case ButtonDisplayStyle.Text:
                        button.Text = command.Name;

                        break;
                    case ButtonDisplayStyle.ImageAndText:
                        button.Text = command.Name;
                        button.Image = ImageExtensions.GetImage(command.ImageName, 16);
                        button.ImagePosition = ButtonImagePosition.Left;
                        break;
                }            

                button.Click += (sender, e) => (button.Tag as IMainViewCommand).Execute(this);

                appGroupLayout.Add(button, true);
            }
                
            appGroupLayout.EndVertical();

            appGroup.Content = appGroupLayout;

            navigationlayout.Add(appGroup);
          
            navigationlayout.BeginVertical();
            navigationlayout.EndVertical();

            var scrollable = new Scrollable()
            {
                Content = navigationlayout
            };

            return scrollable;
        }

        void ShowListViewFromNavigation(NavigationItemDescription item)
        {
            CurrentActiveType = item.ModelType;

            var listLayout = new DynamicLayout();

            listLayout.Add(item.ListView.CreateListViewLayout(this));

            SetContent(listLayout);

            TemplateNavigator.Add(ContentPanel.Content);

            Title = item.NavigationItemText;
        }

        void CreateMenu()
        {
            var menu = new MenuBar();

            Application.Instance.CreateStandardMenu(menu.Items);
            var fileMenu = menu.Items.GetSubmenu("&File");

            var descriptor = MapProvider.Instance.ResolveType<IMainViewDescriptor>();

            foreach (var command in descriptor.Commands)
            {
                var menuItem = new ButtonMenuItem();
                menuItem.Image = ImageExtensions.GetImage(command.ImageName, 12);
                menuItem.Text = command.Name;
                menuItem.ID = command.GetType().FullName;

                menuItem.Click += (sender, e) =>
                {
                    var commandType = Type.GetType(menuItem.ID);
                    if (commandType != null)
                    {
                        var mainCommand = Activator.CreateInstance(commandType) as IMainViewCommand;
                        if (mainCommand != null)
                            mainCommand.Execute(this);
                    }
                };

                fileMenu.Items.Add(menuItem);
            }

            /*
            var help = menu.Items.GetSubmenu("&Help");

            var aboutItem = new ButtonMenuItem();

            aboutItem.Text = "About";
            aboutItem.ID = "cmd_about";

            aboutItem.Click += (sender, e) =>
            {

            };

            help.Items.Add(aboutItem);
            */

            this.Menu = menu;
        }
    }
}
