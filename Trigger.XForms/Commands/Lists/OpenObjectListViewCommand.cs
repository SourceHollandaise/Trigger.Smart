using Trigger.XStorable.DataStore;
using Eto.Forms;
using Trigger.BCL.Common.Model;

namespace Trigger.XForms.Commands
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

        public bool AllowExecute
        {
            get
            {
                if (typeof(User).IsAssignableFrom(listViewArgs.TargetType))
                {
                    return UserQuery.CurrentUserIsAdministrator;
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
