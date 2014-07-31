using System;
using System.Collections.Generic;
using System.Linq;
using Eto.Drawing;
using Eto.Forms;
using Trigger.XStorable.DataStore;
using Trigger.XForms.Controllers;

namespace Trigger.XForms.Visuals
{
    public class MainViewTemplate : TemplateBase
    {
        protected List<Button> CurrentActiveButtons = new List<Button>();

        protected Panel ListViewPanel;
 
        protected Panel NavigationPanel;

        public GridView CurrentGridView
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
            this.WindowState = WindowState.Maximized;

            SetContent();
        }

        void SetContent()
        {
            NavigationPanel = new Panel();
            NavigationPanel.Content = GetNavigationPanelContent();
            ListViewPanel = new Panel();

            var splitter = new Splitter()
            {
                Panel1 = NavigationPanel,
                Panel2 = ListViewPanel,
                Orientation = SplitterOrientation.Horizontal,
                FixedPanel = SplitterFixedPanel.Panel1
            };
            splitter.Panel1.Size = new Size(200, -1);
            splitter.Panel2.Size = new Size(-1, -1);
        }

        DynamicLayout GetNavigationPanelContent()
        {
            var mainLayout = new DynamicLayout();

            foreach (var type in ModelTypesDeclarator.DeclaredModelTypes)
            {
                var navigationButton = CreateNavigationItemButton(type);

                navigationButton.Click += (sender, e) =>
                {
                    CurrentActiveType = navigationButton.Tag as Type;

                    UpdateListCommands(CurrentActiveType);

                    var listViewContent = new DynamicLayout();

                    listViewContent.Add(CreateCommandButtonLayout());

                    listViewContent.Add(CreateListViewLayout());

                    ListViewPanel.Content = listViewContent;
                };

                mainLayout.BeginHorizontal();
                mainLayout.Add(navigationButton, true);
                mainLayout.EndHorizontal();
            }

            mainLayout.BeginHorizontal();
            mainLayout.EndHorizontal();

            return mainLayout;
        }

        DynamicLayout CreateCommandButtonLayout()
        {
            var groupBox = new GroupBox();
            var layout = new DynamicLayout();
            layout.BeginHorizontal();
            foreach (var item in CurrentActiveButtons)
            {
                layout.Add(item, false, false);
            }
            layout.Add(new DynamicLayout(), false, false);
            layout.EndHorizontal();
            groupBox.Content = layout;
            return layout;
        }

        DynamicLayout CreateListViewLayout()
        {
            var groupBox = new GroupBox();
            var layout = new DynamicLayout();
            layout.BeginVertical();
            var dscrType = ListViewDescriptorProvider.GetDescriptor(CurrentActiveType);
            if (dscrType != null)
            {
                var dscr = Activator.CreateInstance(dscrType) as IListViewDescriptor;
                CurrentGridView = new ListViewBuilder(dscr, CurrentActiveType).GetContent();
                CurrentGridView.Size = new Size(-1, -1);
                layout.Add(CurrentGridView);
            }
            layout.EndVertical();
            groupBox.Content = layout;
            return layout;
        }

        Button CreateNavigationItemButton(Type type)
        {
            var displayNameAttribute = type.GetCustomAttributes(typeof(System.ComponentModel.DisplayNameAttribute), true).FirstOrDefault() as System.ComponentModel.DisplayNameAttribute;

            var navigationButton = new Button()
            {
                Size = new Size(-1, 60),
                Text = displayNameAttribute != null ? displayNameAttribute.DisplayName : type.Name,
                Tag = type,
                ImagePosition = ButtonImagePosition.Left
            };
            return navigationButton;
        }

        void UpdateListCommands(Type type)
        {
            CurrentActiveButtons.Clear();
            var contollers = new ActionControllerProvider(this).GetListContentController(type);

            foreach (var controller in contollers)
            {
                var commands = controller.Commands();

                foreach (var command in commands)
                {
                    var commandButton = new Button()
                    {
                        ToolTip = command.ToolBarText,
                        Image = command.Image,
                        ImagePosition = ButtonImagePosition.Overlay,
                        Size = new Size(60, 60)
                    };

                    commandButton.Click += (sender, e) =>
                    {
                        command.Execute();
                    };

                    CurrentActiveButtons.Add(commandButton);
                }
            }
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
