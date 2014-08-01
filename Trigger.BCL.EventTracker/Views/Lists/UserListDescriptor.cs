using Trigger.XForms;
using System.Collections.Generic;
using Trigger.BCL.EventTracker.Model;
using Trigger.BCL.Common.Model;

namespace Trigger.BCL.EventTracker
{

    public class UserListDescriptor : ListViewDescriptor<User>
    {
        public UserListDescriptor()
        {
            ColumnDescriptions = new List<ColumnDescription>
            {
                new ColumnDescription(Fields.GetName(m => m.UserName), 1){ ColumnHeaderText = "User" },
            };
        }
    }

}