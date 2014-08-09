using System;
using System.Collections.Generic;
using Eto.Forms;
using XForms.Store;
using System.Linq;
using Eto.Drawing;
using XForms.Commands;
using XForms.Dependency;

namespace XForms.Design
{

    public class ListViewCommandBarBuilder
    {
        IListViewDescriptor descriptor;

        IEnumerable<IStorable> dataSet;

        public GridView CurrentGridView
        {
            get;
            set;
        }

        bool isRoot;

        public Type ModelType
        {
            get;
            set;
        }

        public ListViewCommandBarBuilder(IListViewDescriptor descriptor, Type modelType, bool isRoot, IEnumerable<IStorable> dataSet, GridView currentGridView = null)
        {
            this.CurrentGridView = currentGridView;
            this.dataSet = dataSet;
            this.isRoot = isRoot;
            this.ModelType = modelType;
            this.descriptor = descriptor;
            
        }

        public DynamicLayout GetContent()
        {
            var commandBar = new DynamicLayout();
            commandBar.BeginHorizontal();
            foreach (var command in descriptor.Commands)
            {
                if (command is ICurrentUserListViewCommand)
                    continue;
                var button = new Button();
                button.Size = new Size(command.Width, 34);
                button.ID = command.ID;
                button.ToolTip = command.Name;
                button.Text = command.Name;
                button.Click += (sender, e) => command.Execute(new ListViewArguments
                {
                    TargetType = ModelType,
                    Grid = CurrentGridView,
                    CustomDataSet = dataSet,
                    InputData = descriptor
                });
                commandBar.Add(button, false, false);
            }
            var currentUserCommand = descriptor.Commands.FirstOrDefault(p => p is ICurrentUserListViewCommand);
            if (currentUserCommand != null && isRoot)
                AddCurrentUserToCommandBar(commandBar, currentUserCommand);
            commandBar.Add(new DynamicLayout()
            {
                Size = new Size(-1, -1)
            });
            commandBar.EndHorizontal();
            return commandBar;
        }

        void AddCurrentUserToCommandBar(DynamicLayout commandBar, IListViewCommand command)
        {
            commandBar.Add(new DynamicLayout() { Size = new Size(40, -1) });

            if (command != null)
            {
                var button = new Button()
                {
                    Size = new Size(command.Width, 34),
                    ID = command.ID,
                    Text = command.Name,
                    Image = ImageExtensions.GetImage(command.ImageName, 16),
                    ImagePosition = ButtonImagePosition.Left
                };

                button.Click += (sender, e) => command.Execute(new ListViewArguments
                {
                    TargetType = ModelType,
                    Grid = CurrentGridView,
                    CustomDataSet = dataSet
                });
                commandBar.Add(button, false, false);
            }
        }
    }
}
