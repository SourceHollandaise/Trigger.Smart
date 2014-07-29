using Trigger.XForms;
using System.Collections.Generic;

namespace Trigger.BCL.EventTracker
{
    public class PersonViewLayoutDemoDescriptor : ViewDescriptor
    {
        public PersonViewLayoutDemoDescriptor()
        {
            TabItemDescriptions = new List<TabItemDescription>
            {
                new TabItemDescription("Person", 1)
                {
                    GroupItemDescriptions = new List<GroupItemDescription>
                    {
                        new GroupItemDescription("Name", 1)
                        {
                            ViewItemOrientation = ViewItemOrientation.Horizontal,
                            ViewItemDescriptions = new List<ViewItemDescription>
                            {
                                new ViewItemDescription("FirstName", 1){ LabelText = "Name", LabelOrientation = LabelOrientation.Top },
                                new ViewItemDescription("MiddleName", 2){ LabelText = "Middle name", LabelOrientation = LabelOrientation.Top },
                                new ViewItemDescription("LastName", 3){ LabelText = "Last name", LabelOrientation = LabelOrientation.Top },
                                new ViewItemDescription(ViewDescriptor.EmptySpaceFieldName, 4){ ShowLabel = false }
                            }
                        },
                        new GroupItemDescription("Address", 2)
                        {
                            ViewItemOrientation = ViewItemOrientation.Horizontal,
                            ViewItemDescriptions = new List<ViewItemDescription>
                            {
                                new ViewItemDescription("PostalCode", 1){ LabelText = "Postal Code", LabelOrientation = LabelOrientation.Top },
                                new ViewItemDescription("City", 2){ LabelText = "City", LabelOrientation = LabelOrientation.Top },
                                new ViewItemDescription("Street", 3){ LabelText = "Address", LabelOrientation = LabelOrientation.Top }
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
