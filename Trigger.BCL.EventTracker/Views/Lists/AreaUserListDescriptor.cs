using XForms.Design;
using System.Collections.Generic;
using Trigger.BCL.EventTracker.Model;
using XForms.Commands;
using XForms.Model;

namespace Trigger.BCL.EventTracker
{
    public class AreaUserListDescriptor : ListViewDescriptor<AreaUser>
    {
        public AreaUserListDescriptor()
        {
            Commands.Clear();

            RegisterCommands<INavigateBackListViewCommand>();

            if (ApplicationQuery.CurrentUserIsAdministrator)
                RegisterCommands<ICreateObjectListViewCommand>();

            RegisterCommands<IRefreshListViewCommand>();

            DefaultSorting = ColumnSorting.Ascending;
            DefaultSortProperty = Fields.GetName(m => m.AreaAlias);

            ColumnDescriptions = new List<ColumnDescription>
            {
                new ColumnDescription(Fields.GetName(m => m.AreaAlias), 1){ ColumnHeaderText = "Area" },
                new ColumnDescription(Fields.GetName(m => m.UserAlias), 2){ ColumnHeaderText = "User" },
            };
        }
    }
}
