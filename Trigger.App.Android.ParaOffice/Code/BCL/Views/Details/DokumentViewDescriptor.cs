using System.Collections.Generic;
using XForms.Commands;
using XForms.Design;

namespace Trigger.BCL.ParaOffice
{

    public class DokumentViewDescriptor : DetailViewDescriptor<Dokument>
    {
        public DokumentViewDescriptor()
        {
            RegisterCommands<IAddFileDetailViewCommand>();

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
                                new ViewItemDescription(Fields.GetName(m => m.Subject), 1){ LabelText = "Datei", Required = true  },
                                new ViewItemDescription(Fields.GetName(m => m.AnlageDatum), 2){ LabelText = "Anlage", Required = true  },
                                new ViewItemDescription(Fields.GetName(m => m.ErledigungDatum), 3){ LabelText = "Erledigung" },
                                new ViewItemDescription(Fields.GetName(m => m.Bemerkung), 4){ LabelText = "Bemerkung", Fill = true },

                            }
                        },
                        new GroupItemDescription("Sachbearbeiter/Akt", 2)
                        {
                            ViewItemDescriptions = new List<ViewItemDescription>
                            {
                                new ViewItemDescription(Fields.GetName(m => m.RA), 1){ LabelText = "RA", Required = true },
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
                                new ViewItemDescription(Fields.GetName(m => m.Status), 3){ LabelText = "Status" }
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
