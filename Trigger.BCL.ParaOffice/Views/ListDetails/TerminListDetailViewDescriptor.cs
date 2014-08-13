using System.Collections.Generic;
using XForms.Design;
using XForms.Commands;

namespace Trigger.BCL.ParaOffice
{
    public class TerminListDetailViewDescriptor : DetailViewDescriptor<Termin>
    {
        public TerminListDetailViewDescriptor()
        {
            Commands.Clear();

            RegisterCommands<IDeleteObjectDetailViewCommand>();
            RegisterCommands<IRefreshDetailViewCommand>();

            AutoSave = true;

            MinHeight = 480;

            GroupItemDescriptions = new List<GroupItemDescription>
            {
                new GroupItemDescription(null, 1)
                {
                    ViewItemDescriptions = new List<ViewItemDescription>
                    {
                        new ViewItemDescription(Fields.GetName(m => m.Betreff), 1){ LabelText = "Betreff"  },
                        new ViewItemDescription(Fields.GetName(m => m.Ort), 2){ LabelText = "Ort" },
                        new ViewItemDescription(Fields.GetName(m => m.BeginnAlias), 3){ LabelText = "Beginn", ReadOnly = true  },
                        new ViewItemDescription(Fields.GetName(m => m.DauerAlias), 4){ LabelText = "Dauer", ReadOnly = true },
                        new ViewItemDescription(Fields.GetName(m => m.Beschreibung), 5){ LabelText = "Beschreibung", Fill = true },
                        new ViewItemDescription(Fields.GetName(m => m.OK), 6){ LabelText = "Erledigt", Required = true  },
                    }
                },
                new GroupItemDescription(null, 2)
                {
                    ViewItemDescriptions = new List<ViewItemDescription>
                    {
                        new ViewItemDescription(Fields.GetName(m => m.Akt), 1){ LabelText = "Akt" },
                        new ViewItemDescription(Fields.GetName(m => m.KlientGegner), 2){ LabelText = "Klient/Gegner", ReadOnly = true },
                        new ViewItemDescription(Fields.GetName(m => m.SB), 3){ LabelText = "SB" },
                        new ViewItemDescription(Fields.GetName(m => m.Erzeuger), 4){ LabelText = "Erzeuger" },
                    }
                },
            };
        }
    }
    
}
