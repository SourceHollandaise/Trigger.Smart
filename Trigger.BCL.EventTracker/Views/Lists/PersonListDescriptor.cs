using System.Collections.Generic;
using Trigger.BCL.EventTracker.Model;
using XForms.Design;

namespace Trigger.BCL.EventTracker
{

    public class PersonListDescriptor : ListViewDescriptor<Person>
    {
        public PersonListDescriptor()
        {
            DefaultSorting = ColumnSorting.Ascending;
            DefaultSortProperty = Fields.GetName(m => m.PersonDisplayName);

//            ListDetailView = true;
//            ListDetailViewColumns = 3;
//            ListDetailViewOrientation = ViewItemOrientation.Vertical;
//            ListDetailViewWithToolbar = true;
//
//            DetailView = new PersonListDetailViewDescriptor();

            ColumnDescriptions = new List<ColumnDescription>
            {
                new ColumnDescription(Fields.GetName(m => m.PersonDisplayName), 1){ ColumnHeaderText = "Person" },
                new ColumnDescription(Fields.GetName(m => m.AddressDisplayName), 2){ ColumnHeaderText = "Address" },
            };
        }
    }
}
