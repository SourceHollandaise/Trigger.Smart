using Trigger.XForms.Controllers;
using System;

namespace Trigger.XForms.Commands
{
    public interface IListViewCommand : IViewCommand
    {
        void Execute(Type type);
    }
}
