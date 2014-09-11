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

            RegisterCommands<IRefreshDetailViewCommand>();
            RegisterCommands<IDeleteObjectDetailViewCommand>();

            AutoSave = true;

            IsTaggable = false;

            MinHeight = 240;

            GroupItemDescriptions = new List<GroupItemDescription>
            {
                new GroupItemDescription(null, 1)
                {

                    ViewItemDescriptions = new List<ViewItemDescription>
                    {
                        //new ViewItemDescription(Fields.GetName(m => m.Person), 1){ LabelText = "Person", ShowLabel = false, LabelOrientation = LabelOrientation.Left  },
                        new ViewItemDescription(Fields.GetName(m => m.Organisation), 2){ LabelText = "Unternehmen", ShowLabel = true, LabelOrientation = LabelOrientation.Left },
                        new ViewItemDescription(Fields.GetName(m => m.MobilTelefon), 3){ LabelText = "Mobil", LabelOrientation = LabelOrientation.Left },
                        new ViewItemDescription(Fields.GetName(m => m.Telefon), 4){ LabelText = "Telefon", LabelOrientation = LabelOrientation.Left },
                        new ViewItemDescription(Fields.GetName(m => m.Email), 5){ LabelText = "E-Mail", LabelOrientation = LabelOrientation.Left },
                        new ViewItemDescription(Fields.GetName(m => m.WebSite), 6){ LabelText = "Web", LabelOrientation = LabelOrientation.Left },
                    }
                },
            };
        }
    }
}
