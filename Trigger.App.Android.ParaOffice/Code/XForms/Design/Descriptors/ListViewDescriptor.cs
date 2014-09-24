using System;
using System.Collections.Generic;
using XForms.Store;
using XForms.Commands;
using XForms.Dependency;

namespace XForms.Design
{
    public abstract class ListViewDescriptor<T> : IListViewDescriptor
    {
        public IList<ColumnDescription> ColumnDescriptions { get; set; }

        public string DefaultSortProperty { get; set; }

        public ColumnSorting DefaultSorting { get; set; }

        public bool AllowColumnReorder { get; set; }

        public bool AllowMultiSelection { get; set; }

        public bool ListShowTags { get; set; }

        public bool ListDetailView { get; set; }

        public bool ListDetailViewWithToolbar { get; set; }

        public bool ShowListDetailViewForLinkedLists { get; set; }

        public bool ShowSearchBox { get; set; }

        public int ListDetailViewColumns { get; set; }

        public IDetailViewDescriptor DetailView { get; set; }

        public ViewItemOrientation ListDetailViewOrientation { get; set; }

        public int? RowHeight { get; set; }

        public IList<IListViewCommand> Commands { get; set; }

        public void RegisterCommands<TCommand>() where  TCommand: IListViewCommand
        {
            if (Commands == null)
                Commands = new List<IListViewCommand>();

            var command = MapProvider.Instance.ResolveType<TCommand>();

            Commands.Add(command);
        }

        protected virtual void RegisterDefaultListCommands()
        {
            RegisterCommands<INavigateBackListViewCommand>();
            RegisterCommands<INavigateHomeListViewCommand>();
            RegisterCommands<ICreateObjectListViewCommand>();
            RegisterCommands<IRefreshListViewCommand>();
        }

        public virtual IEnumerable<IStorable> Repository { get; set; }

        public virtual Func<IStorable, bool> Filter { get; set; }

        protected FieldNames<T> Fields = new FieldNames<T>();

        protected ListViewDescriptor()
        {
            AllowColumnReorder = true;
            AllowMultiSelection = false;
            ListShowTags = true;
            ShowSearchBox = false;

            RegisterDefaultListCommands();

            var defaultPropertyAttribute = typeof(T).FindAttribute<System.ComponentModel.DefaultPropertyAttribute>();
            if (defaultPropertyAttribute != null)
            {
                DefaultSortProperty = defaultPropertyAttribute.Name;
                DefaultSorting = ColumnSorting.Ascending;
            }
        }
    }
}
