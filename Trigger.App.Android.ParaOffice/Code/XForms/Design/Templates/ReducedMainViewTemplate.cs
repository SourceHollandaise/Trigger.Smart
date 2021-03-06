using System;
using System.Linq;
using Eto.Drawing;
using Eto.Forms;
using XForms.Commands;
using XForms.Dependency;
using XForms.Store;

namespace XForms.Design
{
    public sealed class ReducedMainViewTemplate : TemplateBase, IMainViewTemplate
    {
        public Type CurrentActiveType
        { 
            get;
            set;
        }

        public void UpdateNavigation()
        {
            this.Content = GetMainPanelNavigationButtonStyle();
        }

        public ReducedMainViewTemplate() : base(typeof(IStorable), null)
        {
            this.Size = new Size(600, 1024);
            this.Minimizable = true;
            this.Maximizable = true;

            this.Content = GetMainPanelNavigationButtonStyle();
        }

        public void SetContent(Control control)
        {
            this.Content = control;
        }

        Control GetMainPanelNavigationButtonStyle()
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
                        BackgroundColor = Colors.Gray
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
                    BackgroundColor = Colors.Gray
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

            return navigationlayout;
        }

        void ShowListViewFromNavigation(NavigationItemDescription item)
        {
            CurrentActiveType = item.ModelType;

            var content = item.ListView.CreateListViewLayout(this);

            SetContent(content);

            TemplateNavigator.Add(this.Content);

            Title = item.NavigationItemText;
        }
    }
}
