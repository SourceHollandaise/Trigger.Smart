
namespace XForms.Commands
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

        public int Width
        {
            get
            {
                return 160;
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
