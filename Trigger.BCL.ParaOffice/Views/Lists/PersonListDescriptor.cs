using System.Collections.Generic;
using XForms.Design;

namespace Trigger.BCL.ParaOffice
{

    public class PersonListDescriptor : ListViewDescriptor<Person>
    {
        public PersonListDescriptor()
        {
            DefaultSorting = ColumnSorting.Ascending;
            DefaultSortProperty = Fields.GetName(m => m.PersonenName);

            ColumnDescriptions = new List<ColumnDescription>
            {
                new ColumnDescription(Fields.GetName(m => m.PersonenName), 1){ ColumnHeaderText = "Person" },
                new ColumnDescription(Fields.GetName(m => m.Art), 2){ ColumnHeaderText = "Art" },
            };
        }
    }
}
