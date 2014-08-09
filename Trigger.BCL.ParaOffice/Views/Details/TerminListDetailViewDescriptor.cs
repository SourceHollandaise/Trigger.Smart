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

            GroupItemDescriptions = new List<GroupItemDescription>
            {
                new GroupItemDescription("Eintrag", 1)
                {
                    ViewItemDescriptions = new List<ViewItemDescription>
                    {
                        new ViewItemDescription(Fields.GetName(m => m.Betreff), 1){ LabelText = "Betreff", Required = true  },
                        new ViewItemDescription(Fields.GetName(m => m.Ort), 2){ LabelText = "Ort", Required = true },
                        new ViewItemDescription(Fields.GetName(m => m.Beginn), 3){ LabelText = "Beginn", Required = true  },
                        new ViewItemDescription(Fields.GetName(m => m.Ende), 4){ LabelText = "Ende", Required = true },
                        new ViewItemDescription(Fields.GetName(m => m.Beschreibung), 5){ LabelText = "Beschreibung", Fill = true },
                    }
                },
                new GroupItemDescription("Details", 2)
                {
                    ViewItemDescriptions = new List<ViewItemDescription>
                    {
                        new ViewItemDescription(Fields.GetName(m => m.Akt), 1){ LabelText = "Akt" },
                        new ViewItemDescription(Fields.GetName(m => m.KlientGegner), 2){ LabelText = "Klient/Gegner", ReadOnly = true },
                        new ViewItemDescription(Fields.GetName(m => m.SB), 3){ LabelText = "SB", Required = true },
                        new ViewItemDescription(Fields.GetName(m => m.Erzeuger), 4){ LabelText = "Erzeuger", Required = true  },
                    }
                },
            };
        }
    }
    
}