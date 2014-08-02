using Trigger.XStorable.DataStore;

namespace Trigger.XForms.Commands
{

    public class OpenObjectCommand : IOpenObjectCommand
    {
        public void Execute(ListViewArguments args)
        {
            if (args.Grid.SelectedItem != null)
            {
                if (args.Grid.SelectedItem != null)
                    (args.Grid.SelectedItem as IStorable).ShowDetailView();
            }
        }

        public string ID
        {
            get
            {
                return "cmd_open";
            }
        }

        public string Name
        {
            get
            {
                return "Open";
            }
        }

        public string ImageName
        {
            get
            {
                return "window_up";
            }
        }
    }
}
