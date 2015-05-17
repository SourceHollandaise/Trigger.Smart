using XForms.Design;
using System.Collections.Generic;
using Trigger.BCL.EventTracker.Model;
using XForms.Commands;
using XForms.Model;

namespace Trigger.BCL.EventTracker
{

    public class ApplicationUserListDescriptor : ListViewDescriptor<ApplicationUser>
    {
        public ApplicationUserListDescriptor()
        {
            Commands.Clear();

            RegisterCommands<INavigateBackListViewCommand>();

            if (ApplicationQuery.CurrentUserIsAdministrator)
                RegisterCommands<ICreateObjectListViewCommand>();

            RegisterCommands<IRefreshListViewCommand>();

            ListShowTags = false;

            DefaultSorting = ColumnSorting.Ascending;
            DefaultSortProperty = Fields.GetName(m => m.UserName);

//            ListDetailView = true;
//            ListDetailViewWithToolbar = true;
//            ListDetailViewColumns = 3;
//            ListDetailViewOrientation = ViewItemOrientation.Vertical;
//
//            DetailView = new ApplicationUserListDetailViewDescriptor();

            ColumnDescriptions = new List<ColumnDescription>
            {
                new ColumnDescription(Fields.GetName(m => m.Avatar), 1){ ColumnHeaderText = "Avatar", AllowResize = false, AutoSize = false },
                new ColumnDescription(Fields.GetName(m => m.UserName), 1){ ColumnHeaderText = "User" },
                new ColumnDescription(Fields.GetName(m => m.Email), 3){ ColumnHeaderText = "E-Mail" },
                new ColumnDescription(Fields.GetName(m => m.UserSex), 4){ ColumnHeaderText = "Sex" },
                new ColumnDescription(Fields.GetName(m => m.Role), 4){ ColumnHeaderText = "Role" },
            };
        }
    }
}
