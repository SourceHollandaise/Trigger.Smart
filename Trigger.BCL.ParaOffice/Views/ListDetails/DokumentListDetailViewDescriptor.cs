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

            MinHeight = 480;

            GroupItemDescriptions = new List<GroupItemDescription>
            {
                new GroupItemDescription(null, 1)
                {
                    ViewItemOrientation = ViewItemOrientation.Horizontal,
                    ViewItemDescriptions = new List<ViewItemDescription>
                    {
                        new ViewItemDescription(Fields.GetName(m => m.Subject), 1){ LabelText = "Betreff", ShowLabel = false },
                        new ViewItemDescription(Fields.GetName(m => m.AnlageDatum), 2){ LabelText = "Anlage", ShowLabel = false }
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
