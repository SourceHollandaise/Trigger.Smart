using Trigger.XForms;
using System.Collections.Generic;

namespace Trigger.BCL.ParaOffice
{

    public class TerminViewDescriptor : ViewDescriptor<Termin>
    {
        public TerminViewDescriptor()
        {
            TabItemDescriptions = new List<TabItemDescription>
            {
                new TabItemDescription("Termin", 1)
                {
                    GroupItemDescriptions = new List<GroupItemDescription>
                    {
                        new GroupItemDescription("Eintrag", 1)
                        {
                            ViewItemDescriptions = new List<ViewItemDescription>
                            {
                                new ViewItemDescription(Fields.GetName(m => m.Betreff), 1){ LabelText = "Betreff" },
                                new ViewItemDescription(Fields.GetName(m => m.Ort), 2){ LabelText = "Ort" },
                                new ViewItemDescription(Fields.GetName(m => m.Beginn), 3){ LabelText = "Beginn" },
                                new ViewItemDescription(Fields.GetName(m => m.Ende), 4){ LabelText = "Ende" },
                                new ViewItemDescription(Fields.GetName(m => m.Beschreibung), 5){ LabelText = "Beschreibung", Fill = true },
                            }
                        },
                        new GroupItemDescription("Details", 2)
                        {
                            ViewItemDescriptions = new List<ViewItemDescription>
                            {
                                new ViewItemDescription(Fields.GetName(m => m.Akt), 1){ LabelText = "Akt" },
                                new ViewItemDescription(Fields.GetName(m => m.KlientGegner), 2){ LabelText = "Klient/Gegner" },
                                new ViewItemDescription(Fields.GetName(m => m.SB), 3){ LabelText = "SB" },
                                new ViewItemDescription(Fields.GetName(m => m.Erzeuger), 4){ LabelText = "Erzeuger" },
                            }
                        },
                    }
                }
            };
        }
    }
}
