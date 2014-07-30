using System;
using Trigger.XForms;
using System.Collections.Generic;

namespace Trigger.BCL.ParaOffice
{
    public class DokumentViewDescriptor : ViewDescriptor<Dokument>
    {
        public DokumentViewDescriptor()
        {
            TabItemDescriptions = new List<TabItemDescription>
            {
                new TabItemDescription("Dokument", 1)
                {
                    GroupItemDescriptions = new List<GroupItemDescription>
                    {
                        new GroupItemDescription("Informationen", 1)
                        {
                            ViewItemDescriptions = new List<ViewItemDescription>
                            {
                                new ViewItemDescription(Fields.GetName(m => m.Subject), 1){ LabelText = "Datei" },
                                new ViewItemDescription(Fields.GetName(m => m.AnlageDatum), 2){ LabelText = "Anlage" },
                                new ViewItemDescription(Fields.GetName(m => m.ErledigungDatum), 3){ LabelText = "Erledigung" },
                                new ViewItemDescription(Fields.GetName(m => m.Bemerkung), 4){ LabelText = "Bemerkung" },

                            }
                        },
                        new GroupItemDescription("Sachbearbeiter/Akt", 2)
                        {
                            ViewItemDescriptions = new List<ViewItemDescription>
                            {
                                new ViewItemDescription(Fields.GetName(m => m.RA), 1){ LabelText = "RA" },
                                new ViewItemDescription(Fields.GetName(m => m.SK), 2){ LabelText = "SK" },
                                new ViewItemDescription(Fields.GetName(m => m.Akt), 3){ LabelText = "Akt" },
                            }
                        },
                        new GroupItemDescription("Art", 3)
                        {
                            ViewItemDescriptions = new List<ViewItemDescription>
                            {
                                new ViewItemDescription(Fields.GetName(m => m.Art), 1){ LabelText = "Art" },
                                new ViewItemDescription(Fields.GetName(m => m.Medium), 2){ LabelText = "Medium" },
                                new ViewItemDescription(Fields.GetName(m => m.Status), 1){ LabelText = "Status" }
                            }
                        }
                    }
                },
                new TabItemDescription("Vorschau", 2)
                {
                    GroupItemDescriptions = new List<GroupItemDescription>
                    {
                        new GroupItemDescription(null, 1)
                        {
                            Fill = true,
                            ViewItemDescriptions = new List<ViewItemDescription>
                            {
                                new ViewItemDescription(Fields.GetName(m => m.FileName), 1){ LabelText = "Vorschau", ShowLabel = false, Fill = true },
                            }
                        }
                    }
                }
            };
        }
    }
    
}
