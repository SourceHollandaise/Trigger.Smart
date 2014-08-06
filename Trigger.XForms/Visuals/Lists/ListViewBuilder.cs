using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Eto.Drawing;
using Eto.Forms;
using Trigger.BCL.Common.Model;
using Trigger.XStorable.DataStore;
using Trigger.XStorable.Dependency;
using Trigger.XForms.Commands;

namespace Trigger.XForms.Visuals
{
    public class ListViewBuilder
    {
        protected int? CurrentRowIndex = null;

        GridView currentGridView;

        IEnumerable<IStorable> dataSet;

        readonly IEnumerable<IStorable> originalDataSet;

        readonly ListViewCellFactory factory;

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
            factory = new ListViewCellFactory(modelType);
        }

        public Control GetContent()
        {
            currentGridView = null;

            var detailViewLayout = new DynamicLayout();
            detailViewLayout.BeginHorizontal();
            detailViewLayout.Add(AddCommandBarLayout());
            detailViewLayout.EndHorizontal();
            detailViewLayout.BeginHorizontal();

            currentGridView = new GridView();
            currentGridView.AllowColumnReordering = descriptor.AllowColumnReorder;
            currentGridView.AllowMultipleSelection = descriptor.AllowMultiSelection;
            currentGridView.ShowCellBorders = false;

            if (descriptor.ListShowTags)
                currentGridView.Columns.Add(CreateTagColumn());

            foreach (var columnItem in descriptor.ColumnDescriptions.OrderBy(p => p.Index).ToList())
            {
                var gridColumn = CreateColumn(columnItem);
                if (gridColumn != null)
                    currentGridView.Columns.Add(gridColumn);
            }

            if (descriptor.RowHeight.HasValue)
                currentGridView.RowHeight = descriptor.RowHeight.Value;


            currentGridView.DataStore = new DataStoreProvider(descriptor, modelType).CreateDataSet(dataSet);

            currentGridView.SortComparer = new Comparison<object>(new GridViewComparer(descriptor).Compare);

            detailViewLayout.Add(currentGridView);

            detailViewLayout.EndHorizontal();

            RegisterToEvents();
 
            return detailViewLayout;
        }

        DynamicLayout AddCommandBarLayout()
        {
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
                command.Execute(new ListViewArguments
                {
                    TargetType = modelType,
                    Grid = currentGridView,
                    CustomDataSet = originalDataSet
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
                var button = new Button();
                button.Size = new Size(100, 40);
                button.ID = command.ID;
                button.Text = command.Name;
                button.Font = new Font(button.Font.Family, button.Font.Size, FontStyle.Bold);
                button.Image = ImageExtensions.GetImage(command.ImageName, 24);
                button.ImagePosition = ButtonImagePosition.Left;
                button.Click += (sender, e) =>
                    command.Execute(new ListViewArguments
                {
                    TargetType = modelType,
                    Grid = currentGridView,
                    CustomDataSet = originalDataSet
                });
                commandBar.Add(button, false, false);
            }
        }

        void RegisterToEvents()
        {
            currentGridView.SelectionChanged += (sender, e) =>
            {
                CurrentRowIndex = currentGridView.SelectedRows.FirstOrDefault();
            };
            currentGridView.CellFormatting += (sender, e) =>
            {
                if (!(e.Column.DataCell is ImageViewCell))
                    e.Font = new Font(e.Font.Family, e.Font.Size);
                if (!descriptor.IsImageList && descriptor.ListShowTags && e.Column.ID == "TagColumn")
                    e.BackgroundColor = SetTagBackColor(e.Item as IStorable);
            };
            currentGridView.ColumnHeaderClick += (sender, e) =>
            {
                try
                {
                    if (e.Column != null)
                        currentGridView.SortComparer = new Comparison<object>(new GridViewComparer(e.Column).ColumnCompare);
                }
                catch
                {
                    currentGridView.SortComparer = new Comparison<object>(new GridViewComparer(descriptor).Compare);
                }
            };
            currentGridView.MouseDoubleClick += (sender, e) =>
            {
                if (currentGridView.SelectedItem != null)
                {
                    DependencyMapProvider.Instance.ResolveType<IOpenObjectListViewCommand>().Execute(new ListViewArguments
                    {
                        Grid = currentGridView,
                        TargetType = modelType
                    });
                }
            };
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

            gridColumn.AutoSize = columnItem.AutoSize;
            gridColumn.DataCell = cell;
            gridColumn.HeaderText = columnItem.ColumnHeaderText;
            gridColumn.Resizable = columnItem.AllowResize;
            gridColumn.Sortable = columnItem.Sorting != ColumnSorting.None;
            gridColumn.ID = property.Name;
            return gridColumn;
        }

        GridColumn CreateTagColumn()
        {
            return new GridColumn()
            {
                DataCell = new TextBoxCell(),
                HeaderText = "Tag",
                Sortable = true,
                Resizable = false,
                AutoSize = false,
                ID = "TagColumn",
                Width = 36,
                Editable = false
            };          
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
    }
}
