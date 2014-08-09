using System.Collections.Generic;
using XForms.Commands;
using XForms.Design;

namespace Trigger.BCL.ParaOffice
{
    public class DokumentListDetailViewDescriptor : DetailViewDescriptor<Dokument>
    {
        public DokumentListDetailViewDescriptor()
        {
            Commands.Clear();

            RegisterCommands<IDeleteObjectDetailViewCommand>();
            RegisterCommands<IRefreshDetailViewCommand>();
            RegisterCommands<IAddFileDetailViewCommand>();

            AutoSave = true;

            GroupItemDescriptions = new List<GroupItemDescription>
            {
                new GroupItemDescription("Dokument", 1)
                {
                    ViewItemDescriptions = new List<ViewItemDescription>
                    {
                        new ViewItemDescription(Fields.GetName(m => m.Bemerkung), 1){ LabelText = "Bemerkung", ShowLabel = false }
                    }
                },

                new GroupItemDescription(null, 2)
                {
                    Fill = true,
                    ViewItemDescriptions = new List<ViewItemDescription>
                    {
                        new ViewItemDescription(Fields.GetName(m => m.FileName), 1){ LabelText = "Vorschau", ShowLabel = false, Fill = true }
                    }
                },
            };
        }
    }
}
