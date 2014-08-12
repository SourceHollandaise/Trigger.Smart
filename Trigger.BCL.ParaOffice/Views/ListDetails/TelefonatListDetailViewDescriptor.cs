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
                new GroupItemDescription(null, 1)
                {
                    ViewItemDescriptions = new List<ViewItemDescription>
                    {
                        new ViewItemDescription(Fields.GetName(m => m.Beginn), 1){ LabelText = "Datum", Required = true  },
                        new ViewItemDescription(Fields.GetName(m => m.Nummer), 2){ LabelText = "Nummer", Required = true },
                        new ViewItemDescription(Fields.GetName(m => m.Teilnehmer), 3){ LabelText = "Teilnehmer" },
                        new ViewItemDescription(Fields.GetName(m => m.Akt), 4){ LabelText = "Akt" },
                    }
                },
                new GroupItemDescription(null, 2)
                {
                    ViewItemDescriptions = new List<ViewItemDescription>
                    {
                        new ViewItemDescription(Fields.GetName(m => m.SB2), 1){ LabelText = "Für SB", Required = true  },
                        new ViewItemDescription(Fields.GetName(m => m.Status), 2){ LabelText = "Status", Required = true },
                        new ViewItemDescription(Fields.GetName(m => m.SB1), 3){ LabelText = "Telefonist", Required = true },
                        //new ViewItemDescription(Fields.GetName(m => m.Art), 4){ LabelText = "Art", Required = true  }, 
                    }
                },
                new GroupItemDescription(null, 3)
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
