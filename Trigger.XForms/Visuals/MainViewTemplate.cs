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
        protected List<Button> CommandButtons = new List<Button>();

        public Panel ListViewPanel
        {
            get;
            set;
        }

        public Panel NavigationPanel
        {
            get;
            set;
        }

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

            CreateMainContent();
        }

        void CreateMainContent()
        {
            NavigationPanel = new Panel();
            NavigationPanel.Content = GetNavigationPanelContent();
            ListViewPanel = new Panel();

            Splitter splitter = new Splitter();
            splitter.Panel1 = NavigationPanel;
            splitter.Panel1.Size = new Size(200, -1);
            splitter.Panel2 = ListViewPanel;
            splitter.Panel2.Size = new Size(-1, -1);
            splitter.Orientation = SplitterOrientation.Horizontal;
            splitter.FixedPanel = SplitterFixedPanel.Panel1;
            this.Content = splitter;
        }

        DynamicLayout GetNavigationPanelContent()
        {
            DynamicLayout layout = new DynamicLayout();

            foreach (var type in ModelTypesDeclarator.DeclaredModelTypes)
            {
                var displayNameAttribute = type.GetCustomAttributes(typeof(System.ComponentModel.DisplayNameAttribute), true).FirstOrDefault() as System.ComponentModel.DisplayNameAttribute;

                var navigationButton = new Button();
                navigationButton.Size = new Size(-1, 60);
                navigationButton.Text = displayNameAttribute != null ? displayNameAttribute.DisplayName : type.Name;
                navigationButton.Tag = type;
                navigationButton.ImagePosition = ButtonImagePosition.Left;

                navigationButton.Click += (sender, e) =>
                {

                    CurrentActiveType = navigationButton.Tag as Type;

                    UpdateListCommands(CurrentActiveType);

                    var listLayout = new DynamicLayout();

                    var commandButtonGroup = new GroupBox();
                    var commandButtonGroupLayout = new DynamicLayout();
                    commandButtonGroupLayout.BeginHorizontal();

                    foreach (var item in CommandButtons)
                    {
                        commandButtonGroupLayout.Add(item, false, false);
                    }
                    commandButtonGroupLayout.Add(new DynamicLayout(), false, false);
                    commandButtonGroupLayout.EndHorizontal();

                    commandButtonGroup.Content = commandButtonGroupLayout;
                    listLayout.Add(commandButtonGroupLayout);

                    var listGroup = new GroupBox();

                    var listGroupLayout = new DynamicLayout();

                    listGroupLayout.BeginVertical();

                    var descriptorTypeListView = ListViewDescriptorProvider.GetDescriptor(CurrentActiveType);
                    if (descriptorTypeListView != null)
                    {
                        var dscr = Activator.CreateInstance(descriptorTypeListView) as IListViewDescriptor;
                        CurrentGridView = new ListViewBuilder(dscr, CurrentActiveType).GetContent();

                        CurrentGridView.Size = new Size(-1, -1);

                        listGroupLayout.Add(CurrentGridView);
                    }
                   
                    listGroupLayout.EndVertical();
                    listGroup.Content = listGroupLayout;

                    listLayout.Add(listGroupLayout);

                    ListViewPanel.Content = listLayout;
                };

                layout.BeginHorizontal();
                layout.Add(navigationButton, true);
                layout.EndHorizontal();
            }

            layout.BeginHorizontal();
            layout.EndHorizontal();

            return layout;
        }

        void UpdateListCommands(Type type)
        {
            CommandButtons.Clear();
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

                    CommandButtons.Add(commandButton);
                }
            }
        }

        void UpdateDetailCommands(Type type)
        {
            CommandButtons.Clear();
            var contollers = new ActionControllerProvider(this).GetDetailContentController(type);

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

                    CommandButtons.Add(commandButton);
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
