using Trigger.XForms;
using System.Collections.Generic;

namespace Trigger.BCL.EventTracker
{
    public class PersonViewDescriptor : ViewDescriptor
    {
        public PersonViewDescriptor()
        {
            GroupItems = new List<GroupItem>
            {
                new GroupItem("Names", 1)
                {
                    ViewItems = new List<ViewItem>
                    {
                        new ViewItem("FirstName", 1){ LabelText = "Name", ShowLabel = true },
                        new ViewItem("MiddleName", 2){ LabelText = "Middle name", ShowLabel = true },
                        new ViewItem("LastName", 3){ LabelText = "Last name", ShowLabel = true }
                    }
                },
                new GroupItem("Address", 2)
                {
                    ViewItems = new List<ViewItem>
                    {
                        new ViewItem("PostalCode", 1){ LabelText = "Postal Code", ShowLabel = true },
                        new ViewItem("City", 2){ LabelText = "City", ShowLabel = true },
                        new ViewItem("Street", 3){ LabelText = "Address", ShowLabel = true }
                    }
                },
                new GroupItem("Links", 3)
                {
                    ViewItems = new List<ViewItem>
                    {
                        new ViewItem("LinkedContacts", 1){ LabelText = "Contacts", ShowLabel = false }
                    }
                }
            };
        }
    }
}

