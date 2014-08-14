using System.Collections.Generic;
using XForms.Design;
using Trigger.BCL.EventTracker.Model;
using XForms.Commands;

namespace Trigger.BCL.EventTracker
{
    public class AreaListDetailViewDescriptor : DetailViewDescriptor<Area>
    {
        public AreaListDetailViewDescriptor()
        {
            Commands.Clear();

            RegisterCommands<ILinkAreaWithUserDetailViewCommand>();
            RegisterCommands<IRefreshDetailViewCommand>();
            RegisterCommands<IDeleteObjectDetailViewCommand>();

            AutoSave = true;

            IsTaggable = true;

            MinHeight = 540;

            TabItemDescriptions = new List<TabItemDescription>
            {
                new TabItemDescription("Area", 1)
                {
                    GroupItemDescriptions = new List<GroupItemDescription>
                    {
                        new GroupItemDescription(null, 1)
                        {
                            ViewItemDescriptions = new List<ViewItemDescription>
                            {
                                new ViewItemDescription(Fields.GetName(m => m.Name), 1){ LabelText = "Name", ShowLabel = false },

                            }
                        },
                        new GroupItemDescription(null, 2)
                        {
                            Fill = true,

                            ViewItemDescriptions = new List<ViewItemDescription>
                            {
                                new ViewItemDescription(Fields.GetName(m => m.Description), 1){ LabelText = "Description", ShowLabel = false, Fill = true },
                            }
                        }
                    }
                },
                new TabItemDescription("Issues", 2)
                {
                    GroupItemDescriptions = new List<GroupItemDescription>
                    {  
                        new GroupItemDescription(null, 1)
                        {
                            Fill = true,
                            ViewItemDescriptions = new List<ViewItemDescription>
                            {
                                new ViewItemDescription(Fields.GetName(m => m.LinkedIssues), 1){ LabelText = "Issues", ShowLabel = false, Fill = true, ListMode = ListPropertyMode.List }
                            }
                        }
                    }
                },
                new TabItemDescription("Documents", 3)
                {
                    GroupItemDescriptions = new List<GroupItemDescription>
                    {  
                        new GroupItemDescription(null, 1)
                        {
                            Fill = true,
                            ViewItemDescriptions = new List<ViewItemDescription>
                            {
                                new ViewItemDescription(Fields.GetName(m => m.LinkedDocuments), 1){ LabelText = "Documents", ShowLabel = false, Fill = true, ListMode = ListPropertyMode.List }
                            }
                        }
                    }
                },
                new TabItemDescription("Users", 4)
                {
                    GroupItemDescriptions = new List<GroupItemDescription>
                    {  
                        new GroupItemDescription(null, 1)
                        {
                            Fill = true,
                            ViewItemDescriptions = new List<ViewItemDescription>
                            {
                                new ViewItemDescription(Fields.GetName(m => m.LinkedAreaUsers), 1){ LabelText = "Area users", ShowLabel = false, Fill = true, ListMode = ListPropertyMode.List }
                            }
                        }
                    }
                },
            };
        }
    }
    
}
