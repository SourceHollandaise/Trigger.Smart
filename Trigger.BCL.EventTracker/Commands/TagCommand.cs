
using Trigger.XForms.Commands;

namespace Trigger.BCL.EventTracker
{

    public class TagCommand : ITagCommand
    {
        public void Execute(DetailViewArguments args)
        {
            
        }

        public string ID
        {
            get
            {
                return "cmd_tags";
            }
        }

        public string Name
        {
            get
            {
                return "Tag";
            }
        }

        public string ImageName
        {
            get
            {
                return "favorite";
            }
        }
    }
}
