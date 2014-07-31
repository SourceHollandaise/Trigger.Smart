using Trigger.XForms;
using System.Collections.Generic;
using Trigger.BCL.EventTracker.Model;

namespace Trigger.BCL.EventTracker
{
    public class ContactListDescriptor : ListViewDescriptor<Contact>
    {
        public ContactListDescriptor()
        {
            ColumnDescriptions = new List<ColumnDescription>
            {
                new ColumnDescription(Fields.GetName(m => m.ContactType), 2){ ColumnHeaderText = "Type" },
                new ColumnDescription(Fields.GetName(m => m.PhoneNumber), 3){ ColumnHeaderText = "Phone" },
                new ColumnDescription(Fields.GetName(m => m.MobileNumber), 4){ ColumnHeaderText = "Mobile" },
                new ColumnDescription(Fields.GetName(m => m.Email), 5){ ColumnHeaderText = "E-Mail" }
            };
        }
    }
}
