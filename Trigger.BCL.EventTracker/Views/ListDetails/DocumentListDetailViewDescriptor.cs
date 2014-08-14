using System.Collections.Generic;
using XForms.Design;
using Trigger.BCL.EventTracker.Model;
using XForms.Commands;

namespace Trigger.BCL.EventTracker
{
    public class DocumentListDetailViewDescriptor : DetailViewDescriptor<Document>
    {
        public DocumentListDetailViewDescriptor()
        {
            Commands.Clear();

            RegisterCommands<IAddFileDetailViewCommand>();
            RegisterCommands<IRefreshDetailViewCommand>();
            RegisterCommands<IDeleteObjectDetailViewCommand>();

            AutoSave = true;

            IsTaggable = true;

            MinHeight = 540;

            TabItemDescriptions = new List<TabItemDescription>
            {
                new TabItemDescription("Document", 1)
                {
                    GroupItemDescriptions = new List<GroupItemDescription>
                    {
                        new GroupItemDescription("Details", 1)
                        {
                            ViewItemDescriptions = new List<ViewItemDescription>
                            {
                                new ViewItemDescription(Fields.GetName(m => m.Subject), 1){ LabelText = "Name" },
                                new ViewItemDescription(Fields.GetName(m => m.Description), 2){ LabelText = "Description", Fill = true },
                                new ViewItemDescription(Fields.GetName(m => m.User), 3){ LabelText = "From" },
                            }
                        },
                        new GroupItemDescription("Links", 2)
                        {
                            ViewItemDescriptions = new List<ViewItemDescription>
                            {
                                new ViewItemDescription(Fields.GetName(m => m.Area), 1){ LabelText = "Area" },
                                new ViewItemDescription(Fields.GetName(m => m.Issue), 2){ LabelText = "Issue" },
                            }
                        }
                    }
                },
                new TabItemDescription("Preview", 2)
                {
                    GroupItemDescriptions = new List<GroupItemDescription>
                    {
                        new GroupItemDescription(null, 1)
                        {
                            Fill = true,
                            ViewItemDescriptions = new List<ViewItemDescription>
                            {
                                new ViewItemDescription(Fields.GetName(m => m.FileName), 1){ LabelText = "Preview file", ShowLabel = false, Fill = true },
                            }
                        }
                    }
                }
            };
        }
    }
    
}
