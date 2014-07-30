using Trigger.XForms;
using System.Collections.Generic;
using Trigger.BCL.EventTracker.Model;

namespace Trigger.BCL.EventTracker
{
    public class PersonViewLayoutDemoDescriptor : ViewDescriptor<Person>
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
                                new ViewItemDescription(Fields.GetName(m => m.FirstName), 1){ LabelText = "Name", LabelOrientation = LabelOrientation.Top },
                                new ViewItemDescription(Fields.GetName(m => m.MiddleName), 2){ LabelText = "Middle name", LabelOrientation = LabelOrientation.Top },
                                new ViewItemDescription(Fields.GetName(m => m.LastName), 3){ LabelText = "Last name", LabelOrientation = LabelOrientation.Top },
                                new ViewItemDescription(EmptySpaceFieldName, 4){ ShowLabel = false }
                            }
                        },
                        new GroupItemDescription("Address", 2)
                        {
                            ViewItemOrientation = ViewItemOrientation.Horizontal,
                            ViewItemDescriptions = new List<ViewItemDescription>
                            {
                                new ViewItemDescription(Fields.GetName(m => m.PostalCode), 1){ LabelText = "Postal Code", LabelOrientation = LabelOrientation.Top },
                                new ViewItemDescription(Fields.GetName(m => m.City), 2){ LabelText = "City", LabelOrientation = LabelOrientation.Top },
                                new ViewItemDescription(Fields.GetName(m => m.Street), 3){ LabelText = "Address", LabelOrientation = LabelOrientation.Top }
                            }
                        },
                        new GroupItemDescription("Links", 3)
                        {
                            ViewItemDescriptions = new List<ViewItemDescription>
                            {
                                new ViewItemDescription(Fields.GetName(m => m.LinkedContacts), 1){ LabelText = "Contacts", ShowLabel = false }
                            }
                        }
                    }
                }
            };
        }
    }
    
}
