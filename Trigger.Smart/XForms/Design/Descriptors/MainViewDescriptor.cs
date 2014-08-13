using System.Collections.Generic;
using XForms.Commands;
using XForms.Dependency;

namespace XForms.Design
{

    public abstract class MainViewDescriptor : IMainViewDescriptor
    {
        public IList<NavigationGroupItem> NavigationGroups { get; set; }

        public IList<IMainViewCommand> Commands { get; set; }

        public void RegisterCommands<TCommand>()  where TCommand: IMainViewCommand
        {
            if (Commands == null)
                Commands = new List<IMainViewCommand>();

            Commands.Add(MapProvider.Instance.ResolveType<TCommand>());
        }
    }
}
