using Trigger.XForms;
using System.Collections.Generic;
using Trigger.BCL.EventTracker.Model;

namespace Trigger.BCL.EventTracker
{

    public class ContactListDescriptor : ListViewDescriptor<Contact>
    {
        public ContactListDescriptor()
        {
            DefaultSorting = ColumnSorting.Ascending;
            DefaultSortProperty = Fields.GetName(m => m.PersonAlias);

            ColumnDescriptions = new List<ColumnDescription>
            {
                new ColumnDescription(Fields.GetName(m => m.PersonAlias), 1){ ColumnHeaderText = "Person" },
                new ColumnDescription(Fields.GetName(m => m.ContactType), 2){ ColumnHeaderText = "Type" },
                new ColumnDescription(Fields.GetName(m => m.MobileNumber), 3){ ColumnHeaderText = "Mobile" },
                new ColumnDescription(Fields.GetName(m => m.PhoneNumber), 4){ ColumnHeaderText = "Phone" },
                new ColumnDescription(Fields.GetName(m => m.Email), 5){ ColumnHeaderText = "E-Mail" },
            };
        }
    }
}
