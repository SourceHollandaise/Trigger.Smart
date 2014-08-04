using System;
using System.Collections.Generic;
using Trigger.XForms.Commands;
using Trigger.XStorable.DataStore;

namespace Trigger.XForms
{
    public interface IListViewDescriptor
    {
        bool AllowColumnReorder { get; set; }

        bool AllowMultiSelection { get; set; }

        bool ListShowTags { get; set; }

        bool IsImageList { get; set; }

        int? RowHeight { get; set; }

        IList<ColumnDescription> ColumnDescriptions { get; }

        IList<IListViewCommand> Commands { get; }

        void RegisterCommands<TCommand>() where TCommand : IListViewCommand;

        Func<IStorable, bool> Filter { get; set; }
    }
}
