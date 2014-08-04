using Trigger.XForms;
using Trigger.XForms.Commands;
using Trigger.BCL.EventTracker.Model;

namespace Trigger.BCL.EventTracker
{

    public class LinkAreaWithUserDetailViewCommand : ILinkAreaWithUserDetailViewCommand
    {
        public void Execute(DetailViewArguments args)
        {
            if (args.CurrentObject is Area)
            {
                //var store = Trigger.XStorable.Dependency.DependencyMapProvider.Instance.ResolveType<IStore>();

                var areaUser = new AreaUser();
                areaUser.Area = args.CurrentObject as Area;

                areaUser.ShowDetailView();
            }
        }

        public string ID
        {
            get
            {
                return "cmd_link_area_user";
            }
        }

        public string Name
        {
            get
            {
                return "Area - User";
            }
        }

        public string ImageName
        {
            get
            {
                return "user_add";
            }
        }
    }
}
