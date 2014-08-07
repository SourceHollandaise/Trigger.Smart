using Trigger.BCL.EventTracker.Model;
using XForms.Commands;
using XForms.Design;

namespace Trigger.BCL.EventTracker
{

    public class LinkAreaWithUserDetailViewCommand : ILinkAreaWithUserDetailViewCommand
    {
        public void Execute(DetailViewArguments args)
        {
            if (args.CurrentObject is Area)
            {
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
                return "Link Area User";
            }
        }

        public string ImageName
        {
            get
            {
                return "user_add";
            }
        }

        public int Width
        {
            get
            {
                return 120;
            }
        }

        public bool AllowExecute
        {
            get
            {
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
