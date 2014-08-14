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

        public Type ModelType
        {
            get;
            set;
        }

        public ListViewCommandBarBuilder(IListViewDescriptor descriptor, Type modelType, IEnumerable<IStorable> dataSet, GridView currentGridView = null)
        {
            this.CurrentGridView = currentGridView;
            this.dataSet = dataSet;
            this.ModelType = modelType;
            this.descriptor = descriptor;
        }

        public DynamicLayout GetContent()
        {
            var commandBar = new DynamicLayout();
            commandBar.BeginHorizontal();
            foreach (var command in descriptor.Commands)
            {
                var button = new Button();
                button.Size = new Size(command.Width, 34);
                button.ID = command.ID;
                button.ToolTip = command.Name;
                button.BackgroundColor = Colors.Gray;

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

                button.Click += (sender, e) => command.Execute(new ListViewArguments
                {
                    TargetType = ModelType,
                    Grid = CurrentGridView,
                    CustomDataSet = dataSet,
                    InputData = descriptor
                });
                commandBar.Add(button, false, false);
            }
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
