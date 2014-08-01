using System.Collections.Generic;
using System;
using Trigger.XForms.Controllers;
using Trigger.XStorable.Dependency;

namespace Trigger.XForms
{
    public abstract class ListViewDescriptor<T> : IListViewDescriptor
    {
        public IList<ColumnDescription> ColumnDescriptions { get; set; }

        public bool AllowColumnReorder { get; set; }

        public bool AllowMultiSelection { get; set; }

        public IList<IListViewCommand> Commands { get; set; }

        public void RegisterCommands<TCommand>() where  TCommand: IListViewCommand
        {
            if (Commands == null)
                Commands = new List<IListViewCommand>();

            var command = DependencyMapProvider.Instance.ResolveType<TCommand>();

            Commands.Add(command);
        }

        protected FieldNames<T> Fields = new FieldNames<T>();

        protected ListViewDescriptor()
        {
            AllowColumnReorder = true;
            AllowMultiSelection = false;
            RegisterCommands<IRefreshListViewCommand>();
            RegisterCommands<IOpenObjectCommand>();
            RegisterCommands<ICreateObjectCommand>();
        }
    }
}
