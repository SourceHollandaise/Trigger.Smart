using System.Collections.Generic;
using System;
using Trigger.XForms.Controllers;

namespace Trigger.XForms
{
    public interface IListViewDescriptor
    {
        bool AllowColumnReorder { get; set; }

        bool AllowMultiSelection { get; set; }

        IList<ColumnDescription> ColumnDescriptions { get; set; }

        IList<IListViewCommand> Commands { get; }

        void RegisterCommands<T>()  where T: IListViewCommand;
    }
}
