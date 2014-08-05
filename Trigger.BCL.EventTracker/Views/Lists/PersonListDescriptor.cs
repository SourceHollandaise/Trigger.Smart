using Trigger.XForms;
using System.Collections.Generic;
using Trigger.BCL.EventTracker.Model;

namespace Trigger.BCL.EventTracker
{

    public class PersonListDescriptor : ListViewDescriptor<Person>
    {
        public PersonListDescriptor()
        {
            ColumnDescriptions = new List<ColumnDescription>
            {
                new ColumnDescription(Fields.GetName(m => m.PersonDisplayName), 1){ ColumnHeaderText = "Person" },
                new ColumnDescription(Fields.GetName(m => m.AddressDisplayName), 2){ ColumnHeaderText = "Address" },
            };
        }
    }
}
