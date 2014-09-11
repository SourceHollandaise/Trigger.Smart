using System.Collections.Generic;
using XForms.Commands;

namespace XForms.Design
{
    public interface IMainViewDescriptor
    {
        IList<NavigationGroupItem> NavigationGroups { get; set; }

        IList<IMainViewCommand> Commands { get; }

        void RegisterCommands<TCommand>()  where TCommand: IMainViewCommand;
    }
}
