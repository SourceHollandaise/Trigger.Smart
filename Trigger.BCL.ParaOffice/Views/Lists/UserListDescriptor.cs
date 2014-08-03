using Trigger.XForms;
using System.Collections.Generic;
using Trigger.BCL.Common.Model;

namespace Trigger.BCL.ParaOffice
{
    public class UserListDescriptor : ListViewDescriptor<User>
    {
        public UserListDescriptor()
        {
            ListShowTags = false;
            ColumnDescriptions = new List<ColumnDescription>
            {
                new ColumnDescription(Fields.GetName(m => m.UserName), 1){ ColumnHeaderText = "Benutzer" },
                new ColumnDescription(Fields.GetName(m => m.UserSex), 2){ ColumnHeaderText = "Geschlecht" },
                new ColumnDescription(Fields.GetName(m => m.Avatar), 3){ ColumnHeaderText = "Avatar" },
            };
        }
    }
}
