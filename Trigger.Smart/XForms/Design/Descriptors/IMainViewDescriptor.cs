using System.Collections.Generic;
using XForms.Commands;
using System;

namespace XForms.Design
{
    public interface IMainViewDescriptor
    {
        IList<NavigationGroupItem> NavigationGroups { get; set; }

        IList<IMainViewCommand> Commands { get; }

        void RegisterCommands<TCommand>()  where TCommand: IMainViewCommand;

        IEnumerable<Type> RegisteredTypes();
    }
}
