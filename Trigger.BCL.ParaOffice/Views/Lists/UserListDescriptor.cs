using System;
using Trigger.XForms;
using System.Collections.Generic;
using Trigger.BCL.Common.Model;

namespace Trigger.BCL.ParaOffice
{

    public class UserListDescriptor : ListViewDescriptor<User>
    {
        public UserListDescriptor()
        {
            ColumnDescriptions = new List<ColumnDescription>
            {
                new ColumnDescription(Fields.GetName(m => m.UserName), 1){ ColumnHeaderText = "Benutzer" },
                new ColumnDescription(Fields.GetName(m => m.Email), 2){ ColumnHeaderText = "E-Mail" },
            };
        }
    }
}
