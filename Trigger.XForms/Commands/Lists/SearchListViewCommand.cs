
namespace Trigger.XForms.Commands
{

    public class SearchListViewCommand : ISearchListViewCommand
    {

        public void Execute(ListViewArguments listParameter)
        {

        }

        public string ID
        {
            get
            {
                return "cmd_search_list";
            }
        }

        public string Name
        {
            get
            {
                return "Search";
            }
        }

        public string ImageName
        {
            get
            {
                return "";
            }
        }
    }
}
