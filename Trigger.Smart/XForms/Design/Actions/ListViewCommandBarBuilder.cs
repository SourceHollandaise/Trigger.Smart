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
        readonly IListViewDescriptor descriptor;

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

        static string lastSearchString;

        protected SearchBox FullTextSearchBox
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

            commandBar.Add(null);
            commandBar.Add(GetSearchBox(), false, false);

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

        Control GetSearchBox()
        {
            var layout = new DynamicLayout();

            FullTextSearchBox = new SearchBox();
            FullTextSearchBox.Size = new Size(200, -1);
            FullTextSearchBox.PlaceholderText = "Search";
            FullTextSearchBox.Font = new Font(FullTextSearchBox.Font.Family, 14F);
           
            FullTextSearchBox.KeyDown += (sender, e) =>
            {
                if (e.Key == Keys.Enter)
                {
                    if (!string.IsNullOrWhiteSpace(FullTextSearchBox.Text))
                    {
                        lastSearchString = FullTextSearchBox.Text;

                        descriptor.Filter = new Func<IStorable, bool>((args) => args.GetSearchString().ToLower().Contains(FullTextSearchBox.Text.ToLower()));
                    }

                    if (string.IsNullOrWhiteSpace(FullTextSearchBox.Text))
                    {
                        lastSearchString = null;
                        descriptor.Filter = null;
                    }

                    CreateContentBasedOnFilter();
                }
            };

            if (!string.IsNullOrWhiteSpace(lastSearchString))
                FullTextSearchBox.Text = lastSearchString;
            layout.Add(FullTextSearchBox);

            return layout;
        }

        void CreateContentBasedOnFilter()
        {
            if (descriptor.ListDetailView)
            {
                var builder = new ListDetailViewBuilder(descriptor, this.ModelType);
                var content = builder.GetContent();
                descriptor.Filter = null;
                (Application.Instance.MainForm as MainViewTemplate).ContentPanel.Content = content;
            }
            else
            {
                var builder = new ListViewBuilder(descriptor, this.ModelType);
                var content = builder.GetContent();
                descriptor.Filter = null;
                (Application.Instance.MainForm as MainViewTemplate).ContentPanel.Content = content;
            }
        }
    }
}
