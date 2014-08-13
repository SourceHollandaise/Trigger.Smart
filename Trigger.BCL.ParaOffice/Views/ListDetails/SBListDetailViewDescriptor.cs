using System.Collections.Generic;
using XForms.Design;
using XForms.Model;
using XForms.Commands;

namespace Trigger.BCL.ParaOffice
{

    public class SBListDetailViewDescriptor : DetailViewDescriptor<SB>
    {
        public SBListDetailViewDescriptor()
        {
            Commands.Clear();

            RegisterCommands<IDeleteObjectDetailViewCommand>();
            RegisterCommands<IRefreshDetailViewCommand>();

            IsTaggable = false;

            AutoSave = true;

            MinHeight = 240;

            GroupItemDescriptions = new List<GroupItemDescription>
            {
                new GroupItemDescription(null, 1)
                {
                    ViewItemDescriptions = new List<ViewItemDescription>
                    {
                        new ViewItemDescription(Fields.GetName(m => m.ID), 1){ LabelText = "SB-Kürzel", Required = true, ReadOnly = !ApplicationQuery.CurrentUserIsAdministrator },
                        new ViewItemDescription(Fields.GetName(m => m.User), 2){ LabelText = "Benutzer", Required = true, ReadOnly = !ApplicationQuery.CurrentUserIsAdministrator },
                        new ViewItemDescription(Fields.GetName(m => m.Aktiv), 3){ LabelText = "Aktiv" },
                        new ViewItemDescription(Fields.GetName(m => m.TermineAnzeigen), 4){ LabelText = "Termine anzeigen" },
                        new ViewItemDescription(Fields.GetName(m => m.TelefonatAnzeigen), 5){ LabelText = "Telefonate anzeigen" },
                    }
                },
            };
        }
    }
}
