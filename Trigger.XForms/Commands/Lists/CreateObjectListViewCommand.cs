using System;
using Trigger.XStorable.DataStore;
using Trigger.BCL.Common.Model;
using Eto.Forms;

namespace Trigger.XForms.Commands
{

    public class CreateObjectListViewCommand : ICreateObjectListViewCommand
    {
        ListViewArguments listViewArgs;

        public void Execute(ListViewArguments args)
        {
            listViewArgs = args;

            if (!AllowExecute)
            {
                SecurityConfirmationMessages.CreateObjectShow();

                return;
            }

            var target = Activator.CreateInstance(args.TargetType) as IStorable;
            if (target != null)
            {
                target.Initialize();
                target.ShowDetailView();
            }
        }

        public string ID
        {
            get
            {
                return "cmd_create";
            }
        }

        public string Name
        {
            get
            {
                return "New";
            }
        }

        public string ImageName
        {
            get
            {
                return "add";
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
