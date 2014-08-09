using System.Collections.Generic;
using XForms.Design;
using XForms.Commands;

namespace Trigger.BCL.ParaOffice
{

    public class KontaktListDetailViewDescriptor : DetailViewDescriptor<Kontakt>
    {
        public KontaktListDetailViewDescriptor()
        {
            Commands.Clear();

            RegisterCommands<IDeleteObjectDetailViewCommand>();
            RegisterCommands<IRefreshDetailViewCommand>();

            AutoSave = true;

            GroupItemDescriptions = new List<GroupItemDescription>
            {
                new GroupItemDescription("Kontaktdaten", 1)
                {
                    ViewItemDescriptions = new List<ViewItemDescription>
                    {
                        new ViewItemDescription(Fields.GetName(m => m.Person), 1){ LabelText = "Person", Required = true  },
                        new ViewItemDescription(Fields.GetName(m => m.Art), 2){ LabelText = "Art", Required = true },
                        new ViewItemDescription(Fields.GetName(m => m.MobilTelefon), 3){ LabelText = "Mobil" },
                        new ViewItemDescription(Fields.GetName(m => m.Telefon), 4){ LabelText = "Telefon" },
                        new ViewItemDescription(Fields.GetName(m => m.Email), 5){ LabelText = "E-Mail" },
                    }
                },
            };
        }
    }
}
