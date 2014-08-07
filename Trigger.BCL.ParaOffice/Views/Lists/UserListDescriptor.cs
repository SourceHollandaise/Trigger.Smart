using System.Collections.Generic;
using XForms.Design;
using XForms.Model;

namespace Trigger.BCL.ParaOffice
{
    public class UserListDescriptor : ListViewDescriptor<User>
    {
        public UserListDescriptor()
        {
            ListShowTags = false;
            RowHeight = 36;

            DefaultSorting = ColumnSorting.Ascending;
            DefaultSortProperty = Fields.GetName(m => m.UserName);

            ColumnDescriptions = new List<ColumnDescription>
            {
                new ColumnDescription(Fields.GetName(m => m.Avatar), 1){ ColumnHeaderText = "Avatar", AllowResize = false, AutoSize = false },
                new ColumnDescription(Fields.GetName(m => m.UserName), 1){ ColumnHeaderText = "Benutzer" },
                new ColumnDescription(Fields.GetName(m => m.Email), 3){ ColumnHeaderText = "E-Mail" },
                new ColumnDescription(Fields.GetName(m => m.UserSex), 4){ ColumnHeaderText = "Gechlecht" },
                new ColumnDescription(Fields.GetName(m => m.Role), 4){ ColumnHeaderText = "Rolle" },
            };
        }
    }
}
