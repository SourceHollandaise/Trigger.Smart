using XForms.Store;
using XForms.Design;
using XForms.Model;

namespace XForms.Commands
{

    public class OpenObjectListViewCommand : IOpenObjectListViewCommand
    {
        ListViewArguments listViewArgs;

        public void Execute(ListViewArguments args)
        {
            listViewArgs = args;

            if (!AllowExecute)
            {
                ConfirmationMessages.NotAllowedShow();

                return;
            }

            if (args.Grid.SelectedItem != null)
            {
                if (args.Grid.SelectedItem != null)
                    (args.Grid.SelectedItem as IStorable).ShowDetailContentEmbedded();
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

        public int Width
        {
            get
            {
                return 80;
            }
        }

        public bool AllowExecute
        {
            get
            {
                if (typeof(User).IsAssignableFrom(listViewArgs.TargetType))
                {
                    return ApplicationQuery.CurrentUserIsAdministrator;
                }

                return true;
            }
        }

        public bool Visible
        {
            get
            {
                return true;
            }
        }
    }
}
