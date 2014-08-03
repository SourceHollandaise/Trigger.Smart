using System;
using System.Linq;
using Eto.Forms;
using Trigger.XStorable.DataStore;
using System.Reflection;
using System.Collections.Generic;
using Trigger.XStorable.Dependency;
using Eto.Drawing;
using Trigger.XForms.Commands;
using Trigger.BCL.Common.Model;

namespace Trigger.XForms.Visuals
{
    public class ListViewBuilder
    {
        GridView currentGridView;

        readonly ListViewControlFactory factory;

        protected IListViewDescriptor Descriptor
        {
            get;
            set;
        }

        public Type ModelType
        {
            get;
            set;
        }

        protected IEnumerable<IStorable> DataSet
        {
            get;
            set;
        }

        protected IEnumerable<IStorable> OriginalDataSet
        {
            get;
            set;
        }

        public ListViewBuilder(IListViewDescriptor descriptor, Type modelType, IEnumerable<IStorable> dataSet = null)
        {
            this.OriginalDataSet = dataSet;
            this.DataSet = dataSet;
            this.ModelType = modelType;
            this.Descriptor = descriptor;
            factory = new ListViewControlFactory(ModelType);   
        }

        public Control GetContent()
        {
            currentGridView = null;

            var detailViewLayout = new DynamicLayout();
            detailViewLayout.BeginHorizontal();

            var commandBar = new DynamicLayout();
            commandBar.BeginHorizontal();
            foreach (var command in Descriptor.Commands)
            {
                if (command is ICurrentUserListViewCommand)
                    continue;
                var button = new Button();
                button.Size = new Size(40, 40);
                button.ID = command.ID;
                button.ToolTip = command.Name;
                button.Image = ImageExtensions.GetImage(command.ImageName, 24);
                button.ImagePosition = ButtonImagePosition.Overlay;
                button.Click += (sender, e) =>
                {
                    command.Execute(new ListViewArguments{ TargetType = ModelType, Grid = currentGridView, CustomDataSet = OriginalDataSet });
                };
                commandBar.Add(button, false, false);

            }

            //AddSearchBoxToCommandBar(commandBar);

            var currentUserCommand = Descriptor.Commands.FirstOrDefault(p => p is ICurrentUserListViewCommand);
            if (currentUserCommand != null)
                AddCurrentUserToCommandBar(commandBar, currentUserCommand);

            commandBar.Add(new DynamicLayout(){ Size = new Size(-1, -1) });

            commandBar.EndHorizontal();

            detailViewLayout.Add(commandBar);

            detailViewLayout.EndHorizontal();
            detailViewLayout.BeginHorizontal();

            currentGridView = new GridView();
           
            if (Descriptor.ListShowTags)
            {
                var tagColumn = new GridColumn();
                tagColumn.DataCell = new TextBoxCell();
                tagColumn.HeaderText = "Tags";
                tagColumn.Sortable = true;
                tagColumn.Resizable = true;
                tagColumn.AutoSize = false;
                tagColumn.ID = "TagColumn";
                tagColumn.Width = 40;
                tagColumn.Editable = false;

                currentGridView.Columns.Add(tagColumn);
            }

            foreach (var columnItem in Descriptor.ColumnDescriptions.OrderBy(p => p.Index).ToList())
            {
                var gridColumn = CreateColumn(columnItem);
                if (gridColumn != null)
                    currentGridView.Columns.Add(gridColumn);
            }

            if (DataSet == null)
                DataSet = DependencyMapProvider.Instance.ResolveType<IStore>().LoadAll(ModelType).ToList();

            currentGridView.DataStore = new DataStoreCollection(DataSet);
            currentGridView.AllowColumnReordering = Descriptor.AllowColumnReorder;
            currentGridView.AllowMultipleSelection = Descriptor.AllowMultiSelection;
            currentGridView.ShowCellBorders = false;

            currentGridView.CellFormatting += (object sender, GridCellFormatEventArgs e) =>
            {
                if (Descriptor.ListShowTags && e.Column.ID == "TagColumn")
                    e.BackgroundColor = SetTagBackColor(e.Item as IStorable);

                e.Font = new Font(e.Font.Family, 12.5f);
            };

            currentGridView.MouseDoubleClick += (sender, e) =>
            {
                if (currentGridView.SelectedItem != null)
                    (currentGridView.SelectedItem as IStorable).ShowDetailView();
            };

            detailViewLayout.Add(currentGridView);

            detailViewLayout.EndHorizontal();
            return detailViewLayout;
        }

        void AddCurrentUserToCommandBar(DynamicLayout commandBar, IListViewCommand command)
        {
            commandBar.Add(new DynamicLayout(){ Size = new Size(40, -1) });

            if (command != null)
            {
                var button = new Button();
                button.Size = new Size(100, 40);
                button.ID = command.ID;
                button.Text = command.Name;
                button.Font = new Font(button.Font.Family, button.Font.Size, FontStyle.Bold);
                button.Image = ImageExtensions.GetImage(command.ImageName, 24);
                button.ImagePosition = ButtonImagePosition.Left;
                button.Click += (sender, e) =>
                {
                    command.Execute(new ListViewArguments{ TargetType = ModelType, Grid = currentGridView, CustomDataSet = OriginalDataSet });
                };
                commandBar.Add(button, false, false);
            }
        }

        void AddSearchBoxToCommandBar(DynamicLayout commandBar)
        {
            commandBar.Add(new DynamicLayout(){ Size = new Size(60, -1) });

            var displayNameAttribute = ModelType.FindAttribute<System.ComponentModel.DisplayNameAttribute>();

            var searchBox = new SearchBox()
            {
                Size = new Size(160, 40),
            };
                
            searchBox.PlaceholderText = "Search " + (displayNameAttribute != null ? displayNameAttribute.DisplayName : ModelType.Name);
            searchBox.MaxLength = 100;
            searchBox.KeyDown += (sender, e) =>
            {
                if (e.Key == Keys.Enter)
                {
                    var arguments = new ListViewArguments();
                    arguments.Grid = currentGridView;
                    arguments.TargetType = ModelType;
                    arguments.CustomDataSet = DataSet;
                    arguments.InputData = searchBox.Text;
                
                    DependencyMapProvider.Instance.ResolveType<ISearchListViewCommand>().Execute(arguments);
                }
            };

            commandBar.Add(searchBox, false, false);
            commandBar.Add(new DynamicLayout(){ Size = new Size(-1, -1) });
        }

        Color SetTagBackColor(IStorable current)
        {
            var store = DependencyMapProvider.Instance.ResolveType<IStore>();
            var tag = store.LoadAll<Tag>().FirstOrDefault(p => p.TargetObjectMappingId.Equals(current.MappingId.ToString()));
            if (tag != null)
            {
                var rowColor = Color.Parse(tag.TagColor);
                if (rowColor.Equals(Colors.WhiteSmoke))
                    return Colors.White;
                else
                    return rowColor;
                     
            }

            return Colors.White;
        }

        GridColumn CreateColumn(ColumnDescription columnItem)
        {
            var property = ModelType.GetProperty(columnItem.FieldName);
            if (property == null)
                return null;
                
            var cell = factory.CreateDataCell(property);

            if (cell == null)
                return null;

            var gridColumn = new GridColumn();
            gridColumn.DataCell = cell;
            gridColumn.HeaderText = columnItem.ColumnHeaderText;
            gridColumn.Sortable = columnItem.Sorting != ColumnSorting.None;
            gridColumn.Resizable = true;
            gridColumn.AutoSize = true;
           
            return gridColumn;
        }
    }
}
