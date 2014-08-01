using Trigger.XStorable.DataStore;
using Trigger.XForms.Visuals;

namespace Trigger.XForms.Commands
{

    public class OpenObjectCommand : IOpenObjectCommand
    {
        public void Execute(ListViewArguments args)
        {
            if (args.Grid.SelectedItem != null)
            {
                if (args.Grid.SelectedItem != null)
                    WindowManager.ShowDetailView(args.Grid.SelectedItem as IStorable);
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
                return "Search16";
            }
        }
    }
}
