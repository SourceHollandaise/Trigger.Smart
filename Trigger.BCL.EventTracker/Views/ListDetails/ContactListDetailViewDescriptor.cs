using System.Collections.Generic;
using XForms.Design;
using Trigger.BCL.EventTracker.Model;

namespace Trigger.BCL.EventTracker
{

    public class ContactListDetailViewDescriptor : DetailViewDescriptor<Contact>
    {
        public ContactListDetailViewDescriptor()
        {
            Commands.Clear();

            RegisterCommands<XForms.Commands.IDeleteObjectDetailViewCommand>();
            RegisterCommands<XForms.Commands.IRefreshDetailViewCommand>();

            AutoSave = true;

            IsTaggable = false;

            MinHeight = 300;

            GroupItemDescriptions = new List<GroupItemDescription>
            {
                new GroupItemDescription(null, 1)
                {
                    ViewItemDescriptions = new List<ViewItemDescription>
                    {
                        new ViewItemDescription(Fields.GetName(m => m.Person), 1){ LabelText = "Person", ShowLabel = false, LabelOrientation = LabelOrientation.Left },
                        new ViewItemDescription(Fields.GetName(m => m.ContactType), 2){ LabelText = "Type", ShowLabel = false, LabelOrientation = LabelOrientation.Left },
                        new ViewItemDescription(Fields.GetName(m => m.PhoneNumber), 3){ LabelText = "Phone", LabelOrientation = LabelOrientation.Left },
                        new ViewItemDescription(Fields.GetName(m => m.MobileNumber), 4){ LabelText = "Mobile", LabelOrientation = LabelOrientation.Left },
                        new ViewItemDescription(Fields.GetName(m => m.Email), 5){ LabelText = "E-Mail", LabelOrientation = LabelOrientation.Left },
                    }
                }
            };
        }
    }
}
