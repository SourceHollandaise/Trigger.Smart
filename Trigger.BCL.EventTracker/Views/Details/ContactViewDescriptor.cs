using System.Collections.Generic;
using XForms.Design;
using Trigger.BCL.EventTracker.Model;

namespace Trigger.BCL.EventTracker
{
    public class ContactViewDescriptor : DetailViewDescriptor<Contact>
    {
        public ContactViewDescriptor()
        {
            AutoSave = true;

            GroupItemDescriptions = new List<GroupItemDescription>
            {
                new GroupItemDescription("Details", 1)
                {
                    ViewItemDescriptions = new List<ViewItemDescription>
                    {
                        new ViewItemDescription(Fields.GetName(m => m.Person), 1){ LabelText = "Person", ShowLabel = false },
                        new ViewItemDescription(Fields.GetName(m => m.ContactType), 2){ LabelText = "Type", ShowLabel = false },
                        new ViewItemDescription(Fields.GetName(m => m.PhoneNumber), 3){ LabelText = "Phone" },
                        new ViewItemDescription(Fields.GetName(m => m.MobileNumber), 4){ LabelText = "Mobile" },
                        new ViewItemDescription(Fields.GetName(m => m.Email), 5){ LabelText = "E-Mail" },
                    }
                }
            };
        }
    }
}
