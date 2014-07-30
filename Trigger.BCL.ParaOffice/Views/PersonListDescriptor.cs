using System;
using Trigger.XForms;
using System.Collections.Generic;

namespace Trigger.BCL.ParaOffice
{

    public class PersonListDescriptor : ListDescriptor<Person>
    {
        public PersonListDescriptor()
        {
            ColumnDescriptions = new List<ColumnDescription>
            {
                new ColumnDescription(Fields.GetName(m => m.PersonenName), 1){ ColumnHeaderText = "Person" },
                new ColumnDescription(Fields.GetName(m => m.Art), 2){ ColumnHeaderText = "Art" },
            };
        }
    }

}
