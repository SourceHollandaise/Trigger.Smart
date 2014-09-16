using System.Collections.Generic;
using XForms.Design;
using XForms.Model;

namespace Trigger.BCL.ParaOffice
{
    public class SBViewDescriptor : DetailViewDescriptor<SB>
    {
        public SBViewDescriptor()
        {
            IsTaggable = false;

            TabItemDescriptions = new List<TabItemDescription>
            {
                new TabItemDescription("Sachbearbeiter", 1)
                {
                    GroupItemDescriptions = new List<GroupItemDescription>
                    {
                        new GroupItemDescription("Zuordnung", 1)
                        {
                            ViewItemDescriptions = new List<ViewItemDescription>
                            {
                                new ViewItemDescription(Fields.GetName(m => m.ID), 1){ LabelText = "SB-Kürzel", Required = true, ReadOnly = !ApplicationQuery.CurrentUserIsAdministrator },
                                new ViewItemDescription(Fields.GetName(m => m.User), 2){ LabelText = "Benutzer", Required = true, ReadOnly = !ApplicationQuery.CurrentUserIsAdministrator },
                                new ViewItemDescription(Fields.GetName(m => m.ErvCode), 3) { LabelText = "ERV-Code" },
                                new ViewItemDescription(Fields.GetName(m => m.TermineAnzeigen), 4){ LabelText = "Termine anzeigen" },
                                new ViewItemDescription(Fields.GetName(m => m.TelefonatAnzeigen), 5){ LabelText = "Telefonate anzeigen" },
                            }
                        },
                    }
                },
                new TabItemDescription("Termine", 2)
                {
                    GroupItemDescriptions = new List<GroupItemDescription>
                    {
                        new GroupItemDescription(null, 1)
                        {
                            Fill = true,
                            ViewItemDescriptions = new List<ViewItemDescription>
                            {
                                new ViewItemDescription(Fields.GetName(m => m.LinkedTermine), 1){ LabelText = "Termine", ShowLabel = false, Fill = true, ListMode = ListPropertyMode.List }
                            }
                        },
                    }
                },
                new TabItemDescription("Telefonate", 3)
                {
                    GroupItemDescriptions = new List<GroupItemDescription>
                    {
                        new GroupItemDescription(null, 1)
                        {
                            Fill = true,
                            ViewItemDescriptions = new List<ViewItemDescription>
                            {
                                new ViewItemDescription(Fields.GetName(m => m.LinkedTelefonate), 1){ LabelText = "Telefonate", ShowLabel = false, Fill = true, ListMode = ListPropertyMode.List }
                            }
                        },
                    }
                },
                new TabItemDescription("Akten", 4)
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
                        },
                    }
                }
            };
        }
    }

}
