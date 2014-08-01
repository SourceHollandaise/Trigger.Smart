using System.Collections.Generic;
using Trigger.XForms.Commands;

namespace Trigger.XForms
{

    public interface IDetailViewDescriptor
    {
        IList<GroupItemDescription> GroupItemDescriptions { get; set; }

        IList<TabItemDescription> TabItemDescriptions { get; set; }

        IList<IDetailViewCommand> Commands { get; }

        void RegisterCommands<T>()  where T: IDetailViewCommand;
    }
}