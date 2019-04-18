using System;
using XForms.Store;
using XForms.Design;
using XForms.Model;

namespace XForms.Commands
{

    public class CreateObjectListViewCommand : ICreateObjectListViewCommand
    {
        ListViewArguments listViewArgs;

        public void Execute(ListViewArguments args)
        {
            listViewArgs = args;

            if (!AllowExecute)
            {
                ConfirmationMessages.CreateObjectShow();

                return;
            }

            var target = Activator.CreateInstance(args.TargetType) as IStorable;
            if (target != null)
            {
                target.Initialize();
                target.Save();
                target.ShowDetailContentEmbedded();
            }
        }

        public string ID => "cmd_create";

        public ButtonDisplayStyle DisplayStyle => ButtonDisplayStyle.Image;

        public string Name => "Add";

        public string ImageName => "add";

        public int Width => 34;

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
