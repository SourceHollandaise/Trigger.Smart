using System;
using Trigger.XForms;
using System.Collections.Generic;

namespace Trigger.BCL.ParaOffice
{

    public class TelefonatViewDescriptor : DetailViewDescriptor<Telefonat>
    {
        public TelefonatViewDescriptor()
        {
            TabItemDescriptions = new List<TabItemDescription>
            {
                new TabItemDescription("Telefonat", 1)
                {
                    GroupItemDescriptions = new List<GroupItemDescription>
                    {
                        new GroupItemDescription("Zuordnung", 1)
                        {
                            ViewItemDescriptions = new List<ViewItemDescription>
                            {
                                new ViewItemDescription(Fields.GetName(m => m.Beginn), 1){ LabelText = "Datum" },
                                new ViewItemDescription(Fields.GetName(m => m.Nummer), 2){ LabelText = "Nummer" },
                                new ViewItemDescription(Fields.GetName(m => m.Teilnehmer), 3){ LabelText = "Teilnehmer" },
                                new ViewItemDescription(Fields.GetName(m => m.Akt), 4){ LabelText = "Akt" },
                            }
                        },
                        new GroupItemDescription("Details", 2)
                        {
                            ViewItemDescriptions = new List<ViewItemDescription>
                            {
                                new ViewItemDescription(Fields.GetName(m => m.SB1), 1){ LabelText = "Telefonist" },
                                new ViewItemDescription(Fields.GetName(m => m.SB2), 2){ LabelText = "Für SB" },
                                new ViewItemDescription(Fields.GetName(m => m.Art), 3){ LabelText = "Art" },
                                new ViewItemDescription(Fields.GetName(m => m.Status), 4){ LabelText = "Status" },
                            }
                        },
                        new GroupItemDescription("Beschreibung", 3)
                        {
                            Fill = true,
                            ViewItemDescriptions = new List<ViewItemDescription>
                            {
                                new ViewItemDescription(Fields.GetName(m => m.Beschreibung), 1){ LabelText = "Beschreibung", ShowLabel = false, Fill = true },
                            }
                        }
                    }
                }
            };
        }
    }
}
