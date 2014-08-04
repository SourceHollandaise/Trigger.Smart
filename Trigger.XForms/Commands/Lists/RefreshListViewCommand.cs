using System;

namespace Trigger.XForms.Commands
{

    public class RefreshListViewCommand : IRefreshListViewCommand
    {
        public void Execute(ListViewArguments args)
        {
            /*
            IListViewDescriptor descriptor = null;
            var descriptorType = ListViewDescriptorProvider.GetDescriptor(args.TargetType);
            if (descriptorType != null)
            {
                descriptor = Activator.CreateInstance(descriptorType) as IListViewDescriptor;
            }
            */
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
                return "down";
            }
        }
    }
}
