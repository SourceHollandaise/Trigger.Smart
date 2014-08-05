using Trigger.XForms.Visuals;

namespace Trigger.XForms.Commands
{
    public interface IListViewCommand : IViewCommand
    {
        void Execute(ListViewArguments listParameter);
    }

}
