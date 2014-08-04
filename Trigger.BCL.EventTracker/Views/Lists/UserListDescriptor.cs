using Trigger.XForms;
using System.Collections.Generic;
using Trigger.BCL.EventTracker.Model;
using Trigger.BCL.Common.Model;

namespace Trigger.BCL.EventTracker
{

    public class UserListDescriptor : ListViewDescriptor<ApplicationUser>
    {
        public UserListDescriptor()
        {
            ListShowTags = false;
            RowHeight = 36;
            ColumnDescriptions = new List<ColumnDescription>
            {
                new ColumnDescription(Fields.GetName(m => m.Avatar), 1){ ColumnHeaderText = "Avatar", AllowResize = false, AutoSize = false },
                new ColumnDescription(Fields.GetName(m => m.UserName), 1){ ColumnHeaderText = "User" },
                new ColumnDescription(Fields.GetName(m => m.Email), 3){ ColumnHeaderText = "E-Mail" },
                new ColumnDescription(Fields.GetName(m => m.UserSex), 4){ ColumnHeaderText = "Sex" },
            };
        }
    }
}
