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

        public string ID
        {
            get
            {
                return "cmd_create";
            }
        }

        public ButtonDisplayStyle DisplayStyle
        {
            get
            {
                return ButtonDisplayStyle.Image;
            }
        }

        public string Name
        {
            get
            {
                return "Add";
            }
        }

        public string ImageName
        {
            get
            {
                return "add";
            }
        }

        public int Width
        {
            get
            {
                return 34;
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
