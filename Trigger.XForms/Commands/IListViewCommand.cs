using Trigger.XForms.Controllers;
using System;
using Eto.Forms;
using System.Collections.Generic;
using Trigger.XStorable.DataStore;

namespace Trigger.XForms.Commands
{
    public interface IListViewCommand : IViewCommand
    {
        void Execute(ListViewArguments listParameter);
    }
}
