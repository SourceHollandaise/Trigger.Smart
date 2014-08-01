using Trigger.XForms.Controllers;
using Trigger.XStorable.DataStore;

namespace Trigger.XForms.Commands
{
    public interface IDetailViewCommand : IViewCommand
    {
        void Execute(IStorable current);
    }
}
