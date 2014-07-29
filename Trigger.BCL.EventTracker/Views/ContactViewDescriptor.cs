using Trigger.XForms;
using System.Collections.Generic;

namespace Trigger.BCL.EventTracker
{
    public class ContactViewDescriptor : ViewDescriptor
    {
        public ContactViewDescriptor()
        {
            GroupItems = new List<GroupItem>
            {
                new GroupItem("Person", 1)
                {
                    ViewItems = new List<ViewItem>
                    {
                        new ViewItem("Person", 1){ LabelText = "Person", ShowLabel = true }
                    }
                },
                new GroupItem("Contacts", 2)
                {
                    ViewItems = new List<ViewItem>
                    {
                        new ViewItem("ContactType", 1){ LabelText = "Type", ShowLabel = true },
                        new ViewItem("PhoneNumber", 2){ LabelText = "Phone", ShowLabel = false },
                        new ViewItem("MobileNumber", 3){ LabelText = "Mobile phone", ShowLabel = false },
                        new ViewItem("Email", 4){ LabelText = "E-Mail", ShowLabel = false },
                    }
                }
            };
        }
    }
    
}
