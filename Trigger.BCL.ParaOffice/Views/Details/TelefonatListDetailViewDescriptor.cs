using System.Collections.Generic;
using XForms.Design;
using XForms.Commands;

namespace Trigger.BCL.ParaOffice
{

    public class TelefonatListDetailViewDescriptor : DetailViewDescriptor<Telefonat>
    {
        public TelefonatListDetailViewDescriptor()
        {
            Commands.Clear();

            RegisterCommands<IDeleteObjectDetailViewCommand>();
            RegisterCommands<IRefreshDetailViewCommand>();

            AutoSave = true;

            IsTaggable = false;

            GroupItemDescriptions = new List<GroupItemDescription>
            {
                new GroupItemDescription("Zuordnung", 1)
                {
                    ViewItemDescriptions = new List<ViewItemDescription>
                    {
                        new ViewItemDescription(Fields.GetName(m => m.Beginn), 1){ LabelText = "Datum", Required = true  },
                        new ViewItemDescription(Fields.GetName(m => m.Nummer), 2){ LabelText = "Nummer", Required = true },
                        new ViewItemDescription(Fields.GetName(m => m.Teilnehmer), 3){ LabelText = "Teilnehmer" },
                        new ViewItemDescription(Fields.GetName(m => m.Akt), 4){ LabelText = "Akt" },
                    }
                },
                new GroupItemDescription("Details", 2)
                {
                    ViewItemDescriptions = new List<ViewItemDescription>
                    {
                        new ViewItemDescription(Fields.GetName(m => m.SB1), 1){ LabelText = "Telefonist", Required = true },
                        new ViewItemDescription(Fields.GetName(m => m.SB2), 2){ LabelText = "Für SB", Required = true  },
                        new ViewItemDescription(Fields.GetName(m => m.Art), 3){ LabelText = "Art", Required = true  },
                        new ViewItemDescription(Fields.GetName(m => m.Status), 4){ LabelText = "Status", Required = true },
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
            };
        }
    }
    
}
