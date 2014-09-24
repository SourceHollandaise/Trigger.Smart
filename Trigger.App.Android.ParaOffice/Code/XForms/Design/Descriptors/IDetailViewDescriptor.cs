using System.Collections.Generic;
using XForms.Commands;

namespace XForms.Design
{
    public interface IDetailViewDescriptor
    {
        IList<GroupItemDescription> GroupItemDescriptions { get; set; }

        IList<TabItemDescription> TabItemDescriptions { get; set; }

        IList<IDetailViewCommand> Commands { get; }

        int? MinHeight { get; set; }

        bool IsTaggable { get; set; }

        bool AutoSave { get; set; }

        void RegisterCommands<T>()  where T: IDetailViewCommand;
    }
}