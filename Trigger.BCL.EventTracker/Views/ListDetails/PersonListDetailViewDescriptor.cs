using System.Collections.Generic;
using XForms.Design;
using Trigger.BCL.EventTracker.Model;
using XForms.Commands;

namespace Trigger.BCL.EventTracker
{
    public class PersonListDetailViewDescriptor : DetailViewDescriptor<Person>
    {
        public PersonListDetailViewDescriptor()
        {
            Commands.Clear();

            RegisterCommands<IRefreshDetailViewCommand>();
            RegisterCommands<IDeleteObjectDetailViewCommand>();

            AutoSave = true;

            IsTaggable = true;

            MinHeight = 440;

            TabItemDescriptions = new List<TabItemDescription>
            {
                new TabItemDescription("Person", 1)
                {
                    GroupItemDescriptions = new List<GroupItemDescription>
                    {
                        new GroupItemDescription(null, 1)
                        {
                            ViewItemDescriptions = new List<ViewItemDescription>
                            {
                                new ViewItemDescription(Fields.GetName(m => m.FirstName), 1){ LabelText = "Name" },
                                new ViewItemDescription(Fields.GetName(m => m.MiddleName), 2){ LabelText = "Middle name" },
                                new ViewItemDescription(Fields.GetName(m => m.LastName), 3){ LabelText = "Last name" },
                                new ViewItemDescription(Fields.GetName(m => m.Company), 4){ LabelText = "Company" },
                            }
                        },
                        new GroupItemDescription(null, 2)
                        {
                            ViewItemDescriptions = new List<ViewItemDescription>
                            {
                                new ViewItemDescription(Fields.GetName(m => m.PostalCode), 1){ LabelText = "Postal Code" },
                                new ViewItemDescription(Fields.GetName(m => m.City), 2){ LabelText = "City" },
                                new ViewItemDescription(Fields.GetName(m => m.Street), 3){ LabelText = "Address" }
                            }
                        }
                    }
                },
                new TabItemDescription("Contacts", 2)
                {
                    GroupItemDescriptions = new List<GroupItemDescription>
                    {  
                        new GroupItemDescription(null, 1)
                        {
                            Fill = true,
                            ViewItemDescriptions = new List<ViewItemDescription>
                            {
                                new ViewItemDescription(Fields.GetName(m => m.LinkedContacts), 1){ LabelText = "Contacts", ShowLabel = false, Fill = true, ListMode = ListPropertyMode.List }
                            }
                        }
                    }
                },
            };
        }
    }
    
}
