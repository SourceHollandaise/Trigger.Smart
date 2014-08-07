using Trigger.XForms.Visuals;

namespace Trigger.XForms.Commands
{

    public interface IMainViewCommand : IViewCommand
    {
        void Execute(MainViewTemplate template);
    }
}
