using System;
using Trigger.XStorable.DataStore;

namespace Trigger.XForms.Commands
{

    public class CreateObjectListViewCommand : ICreateObjectListViewCommand
    {
        public void Execute(ListViewArguments args)
        {
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
    }
}
