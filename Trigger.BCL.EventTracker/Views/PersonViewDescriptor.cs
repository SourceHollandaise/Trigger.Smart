using Trigger.XForms;
using System.Collections.Generic;

namespace Trigger.BCL.EventTracker
{
    public class PersonViewDescriptor : ViewDescriptor
    {
        public PersonViewDescriptor()
        {
            TabItemDescriptions = new List<TabItemDescription>
            {
                new TabItemDescription("Person", 1)
                {
                    GroupItemDescriptions = new List<GroupItemDescription>
                    {
                        new GroupItemDescription("Name", 1)
                        {
                            ViewItemDescriptions = new List<ViewItemDescription>
                            {
                                new ViewItemDescription("FirstName", 1){ LabelText = "Name" },
                                new ViewItemDescription("MiddleName", 2){ LabelText = "Middle name" },
                                new ViewItemDescription("LastName", 3){ LabelText = "Last name" }
                            }
                        },
                        new GroupItemDescription("Address", 2)
                        {
                            ViewItemDescriptions = new List<ViewItemDescription>
                            {
                                new ViewItemDescription("PostalCode", 1){ LabelText = "Postal Code" },
                                new ViewItemDescription("City", 2){ LabelText = "City" },
                                new ViewItemDescription("Street", 3){ LabelText = "Address" }
                            }
                        },
                        new GroupItemDescription("Links", 3)
                        {
                            ViewItemDescriptions = new List<ViewItemDescription>
                            {
                                new ViewItemDescription("LinkedContacts", 1){ LabelText = "Contacts", ShowLabel = false }
                            }
                        }
                    }
                }
            };
        }
    }
}

