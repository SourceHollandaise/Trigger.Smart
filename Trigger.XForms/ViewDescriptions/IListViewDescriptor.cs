using System;
using System.Collections.Generic;
using Trigger.XForms.Commands;
using Trigger.XStorable.DataStore;

namespace Trigger.XForms
{
    public interface IListViewDescriptor
    {
        string DefaultSortProperty { get; set; }

        bool AllowColumnReorder { get; set; }

        ColumnSorting DefaultSorting { get; set; }

        bool AllowMultiSelection { get; set; }

        bool ListShowTags { get; set; }

        bool IsImageList { get; set; }

        int? RowHeight { get; set; }

        IList<ColumnDescription> ColumnDescriptions { get; }

        IList<IListViewCommand> Commands { get; }

        void RegisterCommands<TCommand>() where TCommand : IListViewCommand;

        IEnumerable<IStorable> Repository { get; }

        Func<IStorable, bool> Filter { get; set; }
    }
}
