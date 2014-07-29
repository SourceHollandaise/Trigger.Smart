using Trigger.XForms;
using System.Collections.Generic;

namespace Trigger.BCL.EventTracker
{
    public class ContactViewDescriptor : ViewDescriptor
    {
        public ContactViewDescriptor()
        {
            GroupItemDescriptions = new List<GroupItemDescription>
            {
                new GroupItemDescription("Person", 1)
                {
                    ViewItemDescriptions = new List<ViewItemDescription>
                    {
                        new ViewItemDescription("Person", 1){ LabelText = "Person", ShowLabel = true }
                    }
                },
                new GroupItemDescription("Contacts", 2)
                {
                    ViewItemDescriptions = new List<ViewItemDescription>
                    {
                        new ViewItemDescription("ContactType", 1){ LabelText = "Type", ShowLabel = true },
                        new ViewItemDescription("PhoneNumber", 2){ LabelText = "Phone", ShowLabel = true },
                        new ViewItemDescription("MobileNumber", 3){ LabelText = "Mobile", ShowLabel = true },
                        new ViewItemDescription("Email", 4){ LabelText = "E-Mail", ShowLabel = true },
                    }
                }
            };
        }
    }
    
}
