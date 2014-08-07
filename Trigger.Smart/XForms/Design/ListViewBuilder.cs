using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Eto.Drawing;
using Eto.Forms;
using XForms.Store;
using XForms.Dependency;
using XForms.Commands;
using XForms.Model;

namespace XForms.Design
{
    public class ListViewBuilder
    {
        protected int? CurrentRowIndex = null;

        IEnumerable<IStorable> dataSet;

        readonly IEnumerable<IStorable> originalDataSet;

        readonly ListViewCellFactory factory;

        readonly IListViewDescriptor descriptor;

        readonly bool isRoot;

        public Type ModelType
        {
            get;
            private set;
        }

        public GridView CurrentGridView
        {
            get;
            private set;
        }

        public ListViewBuilder(IListViewDescriptor descriptor, Type modelType, bool viewIsRoot = true, IEnumerable<IStorable> dataSet = null)
        {
            this.originalDataSet = dataSet;
            this.dataSet = dataSet;
            this.ModelType = modelType;
            this.descriptor = descriptor;
            this.isRoot = viewIsRoot;
            factory = new ListViewCellFactory(modelType);
        }

        public Control GetContent()
        {
            CurrentGridView = null;

            var detailViewLayout = new DynamicLayout();
            detailViewLayout.BeginHorizontal();
            detailViewLayout.Add(AddCommandBarLayout());
            detailViewLayout.EndHorizontal();
            detailViewLayout.BeginHorizontal();

            CurrentGridView = new GridView();
            CurrentGridView.AllowColumnReordering = descriptor.AllowColumnReorder;
            CurrentGridView.AllowMultipleSelection = descriptor.AllowMultiSelection;
            CurrentGridView.ShowCellBorders = false;

            if (descriptor.ListShowTags)
                CurrentGridView.Columns.Add(CreateTagColumn());

            foreach (var columnItem in descriptor.ColumnDescriptions.OrderBy(p => p.Index).ToList())
            {
                var gridColumn = CreateColumn(columnItem);
                if (gridColumn != null)
                    CurrentGridView.Columns.Add(gridColumn);
            }

            if (descriptor.RowHeight.HasValue)
                CurrentGridView.RowHeight = descriptor.RowHeight.Value;

            CurrentGridView.DataStore = new DataStoreProvider(descriptor, ModelType).CreateDataSet(dataSet);

            CurrentGridView.SortComparer = new Comparison<object>(new GridViewComparer(descriptor).Compare);

            detailViewLayout.Add(CurrentGridView);

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
                button.Size = new Size(command.Width, 34);
                button.ID = command.ID;
                button.ToolTip = command.Name;
                button.Text = command.Name;
                //button.Image = ImageExtensions.GetImage(command.ImageName, 16);
                //button.ImagePosition = ButtonImagePosition.Overlay;
                button.Click += (sender, e) => command.Execute(new ListViewArguments
                {
                    TargetType = ModelType,
                    Grid = CurrentGridView,
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
                button.Size = new Size(command.Width, 34);
                button.ID = command.ID;
                button.Text = command.Name;

                /*
                try
                {
                    button.Font = new Font(button.Font.Family, button.Font.Size, FontStyle.Bold);
                }
                catch
                {

                }
                */
                button.Image = ImageExtensions.GetImage(command.ImageName, 16);
                button.ImagePosition = ButtonImagePosition.Left;
                button.Click += (sender, e) =>
                    command.Execute(new ListViewArguments
                {
                    TargetType = ModelType,
                    Grid = CurrentGridView,
                    CustomDataSet = originalDataSet
                });
                commandBar.Add(button, false, false);
            }
        }

        void RegisterToEvents()
        {
            CurrentGridView.SelectionChanged += (sender, e) =>
            {
                CurrentRowIndex = CurrentGridView.SelectedRows.FirstOrDefault();
            };
            CurrentGridView.CellFormatting += (sender, e) =>
            {
                if (!descriptor.IsImageList && descriptor.ListShowTags && e.Column.ID == "TagColumn")
                {
                    if (e.Item != null)
                        e.BackgroundColor = SetTagBackColor(e.Item as IStorable);
                }
            };
            CurrentGridView.ColumnHeaderClick += (sender, e) =>
            {
                try
                {
                    if (e.Column != null)
                        CurrentGridView.SortComparer = new Comparison<object>(new GridViewComparer(e.Column).ColumnCompare);
                }
                catch
                {
                    CurrentGridView.SortComparer = new Comparison<object>(new GridViewComparer(descriptor).Compare);
                }
            };

            if (!isRoot)
            {
                CurrentGridView.MouseDoubleClick += (sender, e) =>
                {
                    if (CurrentGridView.SelectedItem != null)
                    {
                        MapProvider.Instance.ResolveType<IOpenObjectListViewCommand>().Execute(new ListViewArguments
                        {
                            Grid = CurrentGridView,
                            TargetType = ModelType
                        });
      
                    }
                };
            }
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
                Width = 34,
                Editable = false
            };          
        }

        Color SetTagBackColor(IStorable current)
        {
            if (current == null)
                return Colors.White;

            var store = MapProvider.Instance.ResolveType<IStore>();

            var mappingString = current.MappingId != null ? current.MappingId as string : null;
            if (mappingString == null)
                return Colors.White;

            var tag = store.LoadAll<Tag>().FirstOrDefault(p => !string.IsNullOrEmpty(p.TargetObjectMappingId) && p.TargetObjectMappingId == mappingString);
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
