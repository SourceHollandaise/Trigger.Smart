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
                                new ViewItemDescription("FirstName", 1){ LabelText = "Name", ShowLabel = true },
                                new ViewItemDescription("MiddleName", 2){ LabelText = "Middle name", ShowLabel = true },
                                new ViewItemDescription("LastName", 3){ LabelText = "Last name", ShowLabel = true }
                            }
                        },
                        new GroupItemDescription("Address", 2)
                        {
                            ViewItemDescriptions = new List<ViewItemDescription>
                            {
                                new ViewItemDescription("PostalCode", 1){ LabelText = "Postal Code", ShowLabel = true },
                                new ViewItemDescription("City", 2){ LabelText = "City", ShowLabel = true },
                                new ViewItemDescription("Street", 3){ LabelText = "Address", ShowLabel = true }
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

