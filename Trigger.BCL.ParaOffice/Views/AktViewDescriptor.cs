using System;
using Trigger.XForms;
using System.Collections.Generic;

namespace Trigger.BCL.ParaOffice
{
    public class AktViewDescriptor : ViewDescriptor<Akt>
    {
        public AktViewDescriptor()
        {
            TabItemDescriptions = new List<TabItemDescription>
            {
                new TabItemDescription("Akt", 1)
                {
                    GroupItemDescriptions = new List<GroupItemDescription>
                    {
                        new GroupItemDescription("Informationen", 1)
                        {
                            ViewItemDescriptions = new List<ViewItemDescription>
                            {
                                new ViewItemDescription(Fields.GetName(m => m.Bezeichnung), 1){ LabelText = "Name" },
                                new ViewItemDescription(Fields.GetName(m => m.AktArt), 2){ LabelText = "Art" },
                                new ViewItemDescription(Fields.GetName(m => m.Bemerkung), 3){ LabelText = "Bemerkung" },

                            }
                        },
                        new GroupItemDescription("Sachbearbeiter", 2)
                        {
                            ViewItemDescriptions = new List<ViewItemDescription>
                            {
                                new ViewItemDescription(Fields.GetName(m => m.SB1), 1){ LabelText = "SB 1" },
                                new ViewItemDescription(Fields.GetName(m => m.SB2), 2){ LabelText = "SB 2" },
                                new ViewItemDescription(Fields.GetName(m => m.SB3), 3){ LabelText = "SB 3" },
                            }
                        },
                        new GroupItemDescription("Anlage/Erledigung", 3)
                        {
                            ViewItemDescriptions = new List<ViewItemDescription>
                            {
                                new ViewItemDescription(Fields.GetName(m => m.AnlageDatum), 1){ LabelText = "Anlage" },
                                new ViewItemDescription(Fields.GetName(m => m.ErledigungDatum), 2){ LabelText = "Abschluss" },
                                new ViewItemDescription(Fields.GetName(m => m.ArchivZahl), 1){ LabelText = "Archiv" }
                            }
                        }
                    }
                },
                new TabItemDescription("Dokumente", 2)
                {
                    GroupItemDescriptions = new List<GroupItemDescription>
                    {  
                        new GroupItemDescription(null, 1)
                        {
                            Fill = true,
                            ViewItemDescriptions = new List<ViewItemDescription>
                            {
                                new ViewItemDescription(Fields.GetName(m => m.LinkedDokumente), 1){ LabelText = "Dokumente", ShowLabel = false, Fill = true, ListMode = ListPropertyMode.List }
                            }
                        }
                    }
                },
                new TabItemDescription("Termine", 3)
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
                        }
                    }
                },
                new TabItemDescription("Kontakte", 4)
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
                }
            };
        }
    }
}

