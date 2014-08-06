using Trigger.XForms;
using System.Collections.Generic;

namespace Trigger.BCL.ParaOffice
{
    public class PersonViewDescriptor : DetailViewDescriptor<Person>
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
                                new ViewItemDescription(Fields.GetName(m => m.Vorname), 1){ LabelText = "Vorname (Name 1)", Required = true  },
                                new ViewItemDescription(Fields.GetName(m => m.Nachname), 2){ LabelText = "Nachname (Name 2)", Required = true },
                                new ViewItemDescription(Fields.GetName(m => m.Titel), 3){ LabelText = "Titel" },
                                new ViewItemDescription(Fields.GetName(m => m.Anrede), 4){ LabelText = "Anrede" },
                            }
                        },
                        new GroupItemDescription("Adresse", 2)
                        {
                            ViewItemDescriptions = new List<ViewItemDescription>
                            {
                                new ViewItemDescription(Fields.GetName(m => m.PLZ), 1){ LabelText = "Postleitzahl" },
                                new ViewItemDescription(Fields.GetName(m => m.Ort), 2){ LabelText = "Ort" },
                                new ViewItemDescription(Fields.GetName(m => m.Strasse), 3){ LabelText = "Straße" },
                            }
                        },
                    }
                },
                new TabItemDescription("Kontakte", 2)
                {
                    GroupItemDescriptions = new List<GroupItemDescription>
                    {  
                        new GroupItemDescription(null, 1)
                        {
                            Fill = true,
                            ViewItemDescriptions = new List<ViewItemDescription>
                            {
                                new ViewItemDescription(Fields.GetName(m => m.LinkedKontakte), 1){ LabelText = "Kontakte", ShowLabel = false, Fill = true, ListMode = ListPropertyMode.List }
                            }
                        }
                    }
                },
                new TabItemDescription("Akten", 3)
                {
                    GroupItemDescriptions = new List<GroupItemDescription>
                    {  
                        new GroupItemDescription(null, 1)
                        {
                            Fill = true,
                            ViewItemDescriptions = new List<ViewItemDescription>
                            {
                                new ViewItemDescription(Fields.GetName(m => m.LinkedAkten), 1){ LabelText = "Akten", ShowLabel = false, Fill = true, ListMode = ListPropertyMode.List }
                            }
                        }
                    }
                }
            };
        }
    }
}
