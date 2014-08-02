using System.Collections.Generic;
using Trigger.XForms.Commands;

namespace Trigger.XForms
{

    public interface IDetailViewDescriptor
    {
        IList<GroupItemDescription> GroupItemDescriptions { get; set; }

        IList<TabItemDescription> TabItemDescriptions { get; set; }

        IList<IDetailViewCommand> Commands { get; }

        bool IsTaggable { get; set; }

        void RegisterCommands<T>()  where T: IDetailViewCommand;
    }
}