using System.Collections.Generic;
using Trigger.XForms;
using Trigger.BCL.EventTracker.Model;

namespace Trigger.BCL.EventTracker
{
    public class ContactViewDescriptor : DetailViewDescriptor<Contact>
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
                                new ViewItemDescription(Fields.GetName(m => m.Person), 1){ LabelText = "Person", ShowLabel = false }
                            }
                        },
                        new GroupItemDescription("Type", 2)
                        {
                            ViewItemDescriptions = new List<ViewItemDescription>
                            {
                                new ViewItemDescription(Fields.GetName(m => m.ContactType), 1){ LabelText = "Type", ShowLabel = false },
                            }
                        },
                        new GroupItemDescription("Contact items", 3)
                        {
                            ViewItemDescriptions = new List<ViewItemDescription>
                            {
                                new ViewItemDescription(Fields.GetName(m => m.PhoneNumber), 1){ LabelText = "Phone" },
                                new ViewItemDescription(Fields.GetName(m => m.MobileNumber), 2){ LabelText = "Mobile" },
                                new ViewItemDescription(Fields.GetName(m => m.Email), 3){ LabelText = "E-Mail" },
                            }
                        }
                    }
                }
            };
        }
    }
}
