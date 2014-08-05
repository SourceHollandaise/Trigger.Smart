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
        protected int? CurrentRowIndex = null;

        GridView currentGridView;

        IEnumerable<IStorable> dataSet;

        readonly IEnumerable<IStorable> originalDataSet;

        readonly ListViewControlFactory factory;

        readonly IListViewDescriptor descriptor;

        readonly Type modelType;

        readonly bool isRoot;

        public ListViewBuilder(IListViewDescriptor descriptor, Type modelType, bool viewIsRoot = true, IEnumerable<IStorable> dataSet = null)
        {
            this.originalDataSet = dataSet;
            this.dataSet = dataSet;
            this.modelType = modelType;
            this.descriptor = descriptor;
            this.isRoot = viewIsRoot;
            factory = new ListViewControlFactory(modelType);
        }

        public Control GetContent()
        {
            currentGridView = null;

            var detailViewLayout = new DynamicLayout();
            detailViewLayout.BeginHorizontal();

            var commandBar = new DynamicLayout();
            commandBar.BeginHorizontal();
            foreach (var command in descriptor.Commands)
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
                    command.Execute(new ListViewArguments { TargetType = modelType, Grid = currentGridView, CustomDataSet = originalDataSet });
                };
                commandBar.Add(button, false, false);

            }

            //AddSearchBoxToCommandBar(commandBar);

            var currentUserCommand = descriptor.Commands.FirstOrDefault(p => p is ICurrentUserListViewCommand);
            if (currentUserCommand != null && isRoot)
                AddCurrentUserToCommandBar(commandBar, currentUserCommand);

            commandBar.Add(new DynamicLayout() { Size = new Size(-1, -1) });

            commandBar.EndHorizontal();

            detailViewLayout.Add(commandBar);

            detailViewLayout.EndHorizontal();
            detailViewLayout.BeginHorizontal();

            currentGridView = new GridView();

            if (descriptor.ListShowTags)
            {
                var tagColumn = new GridColumn();
                tagColumn.DataCell = new TextBoxCell();
                tagColumn.HeaderText = "Tag";
                tagColumn.Sortable = true;
                tagColumn.Resizable = false;
                tagColumn.AutoSize = false;
                tagColumn.ID = "TagColumn";
                tagColumn.Width = 36;
                tagColumn.Editable = false;

                currentGridView.Columns.Add(tagColumn);
            }

            foreach (var columnItem in descriptor.ColumnDescriptions.OrderBy(p => p.Index).ToList())
            {
                var gridColumn = CreateColumn(columnItem);
                if (gridColumn != null)
                    currentGridView.Columns.Add(gridColumn);
            }

            CreateDataSet();

            currentGridView.AllowColumnReordering = descriptor.AllowColumnReorder;
            currentGridView.AllowMultipleSelection = descriptor.AllowMultiSelection;
            currentGridView.ShowCellBorders = false;

            if (descriptor.RowHeight.HasValue)
            {
                currentGridView.RowHeight = descriptor.RowHeight.Value;
            }

            currentGridView.SelectionChanged += (sender, e) =>
            {
                CurrentRowIndex = currentGridView.SelectedRows.FirstOrDefault();
               
            };
          
            currentGridView.CellFormatting += (sender, e) =>
            {
                if (!(e.Column.DataCell is ImageViewCell))
                {
                    e.Font = new Font(e.Font.Family, e.Font.Size);
                }
                if (CurrentRowIndex.HasValue && e.Row == CurrentRowIndex)
                {
                    if (!(e.Column.DataCell is ImageViewCell))
                    {
                        //e.Font = new Font(e.Font.Family, e.Font.Size, FontStyle.Bold);
                    }
                }
                else
                {
                    if (!(e.Column.DataCell is ImageViewCell))
                    {
                        //e.Font = new Font(e.Font.Family, e.Font.Size);
                    }
                }
                if (!descriptor.IsImageList && descriptor.ListShowTags && e.Column.ID == "TagColumn")
                    e.BackgroundColor = SetTagBackColor(e.Item as IStorable);

            };
                
            currentGridView.MouseDoubleClick += (sender, e) =>
            {
                if (currentGridView.SelectedItem != null)
                {
                    DependencyMapProvider.Instance.ResolveType<IOpenObjectListViewCommand>().Execute(new ListViewArguments { Grid = currentGridView, TargetType = modelType });
                }
            };

            detailViewLayout.Add(currentGridView);

            detailViewLayout.EndHorizontal();
            return detailViewLayout;
        }

        void CustomizeCell(GridCellFormatEventArgs e)
        {

        }

        void CreateDataSet()
        {
            if (dataSet == null)
            {
                var set = descriptor.Repository ?? DependencyMapProvider.Instance.ResolveType<IStore>().LoadAll(modelType);

                if (descriptor.Filter != null)
                    set = set.Where(descriptor.Filter);

                dataSet = set;
            }

            currentGridView.DataStore = new DataStoreCollection(dataSet);
        }

        void AddCurrentUserToCommandBar(DynamicLayout commandBar, IListViewCommand command)
        {
            commandBar.Add(new DynamicLayout() { Size = new Size(40, -1) });

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
                    command.Execute(new ListViewArguments
                    {
                        TargetType = modelType,
                        Grid = currentGridView,
                        CustomDataSet = originalDataSet
                    });
                };
                commandBar.Add(button, false, false);
            }
        }

        void AddSearchBoxToCommandBar(DynamicLayout commandBar)
        {
            commandBar.Add(new DynamicLayout() { Size = new Size(60, -1) });

            var displayNameAttribute = modelType.FindAttribute<System.ComponentModel.DisplayNameAttribute>();

            var searchBox = new SearchBox()
            {
                Size = new Size(160, 40),
            };

            searchBox.PlaceholderText = "Search " + (displayNameAttribute != null ? displayNameAttribute.DisplayName : modelType.Name);
            searchBox.MaxLength = 100;
            searchBox.KeyDown += (sender, e) =>
            {
                if (e.Key == Keys.Enter)
                {
                    var arguments = new ListViewArguments();
                    arguments.Grid = currentGridView;
                    arguments.TargetType = modelType;
                    arguments.CustomDataSet = dataSet;
                    arguments.InputData = searchBox.Text;

                    DependencyMapProvider.Instance.ResolveType<ISearchListViewCommand>().Execute(arguments);
                }
            };

            commandBar.Add(searchBox, false, false);
            commandBar.Add(new DynamicLayout() { Size = new Size(-1, -1) });
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
                return rowColor;
            }

            return Colors.White;
        }

        GridColumn CreateColumn(ColumnDescription columnItem)
        {
            var property = modelType.GetProperty(columnItem.FieldName);
            if (property == null)
                return null;

            var cell = factory.CreateDataCell(property);

            if (cell == null)
                return null;

            var gridColumn = new GridColumn();
            gridColumn.DataCell = cell;
            gridColumn.HeaderText = columnItem.ColumnHeaderText;
            gridColumn.Sortable = columnItem.Sorting != ColumnSorting.None;
            gridColumn.Resizable = columnItem.AllowResize;
            gridColumn.AutoSize = columnItem.AutoSize;

            return gridColumn;
        }
    }
}
