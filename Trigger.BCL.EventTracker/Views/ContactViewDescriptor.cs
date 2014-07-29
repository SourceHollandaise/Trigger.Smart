using Trigger.XForms;
using System.Collections.Generic;

namespace Trigger.BCL.EventTracker
{
    public class ContactViewDescriptor : ViewDescriptor
    {
        public ContactViewDescriptor()
        {
            TabItemDescriptions = new List<TabItemDescription>
            {
                new TabItemDescription("Contact", 1)
                {
                    GroupItemDescriptions = new List<GroupItemDescription>
                    {
                        new GroupItemDescription("Person", 1)
                        {
                            ViewItemDescriptions = new List<ViewItemDescription>
                            {
                                new ViewItemDescription("Person", 1){ LabelText = "Person", ShowLabel = false }
                            }
                        },
                        new GroupItemDescription("Type", 2)
                        {
                            ViewItemDescriptions = new List<ViewItemDescription>
                            {
                                new ViewItemDescription("ContactType", 1){ LabelText = "Type", ShowLabel = false },
                            }
                        },
                        new GroupItemDescription("Contact items", 3)
                        {
                            ViewItemDescriptions = new List<ViewItemDescription>
                            {
                                new ViewItemDescription("PhoneNumber", 1){ LabelText = "Phone", ShowLabel = true },
                                new ViewItemDescription("MobileNumber", 2){ LabelText = "Mobile", ShowLabel = true },
                                new ViewItemDescription("Email", 3){ LabelText = "E-Mail", ShowLabel = true },
                            }
                        }
                    }
                }
            };
        }
    }
    
}
