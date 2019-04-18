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

        public string ID => "cmd_open";

        public ButtonDisplayStyle DisplayStyle => ButtonDisplayStyle.Image;

        public string Name => "Open";

        public string ImageName => "window_up";

        public int Width => 70;

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

        public bool Visible => true;
    }
}
