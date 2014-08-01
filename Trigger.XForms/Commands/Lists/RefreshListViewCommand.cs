using System;
using Trigger.XStorable.DataStore;
using Trigger.XStorable.Dependency;
using Eto.Forms;

namespace Trigger.XForms.Commands
{

    public class RefreshListViewCommand : IRefreshListViewCommand
    {
        public void Execute(ListViewArguments args)
        {
            args.Grid.ReloadList(args.TargetType, args.CustomDataSet);
        }

        public string ID
        {
            get
            {
                return "cmd_refresh";
            }
        }

        public string Name
        {
            get
            {
                return "Refresh";
            }
        }

        public string ImageName
        {
            get
            {
                return "Refresh16";
            }
        }
    }
}
