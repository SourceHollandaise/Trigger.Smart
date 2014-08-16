
namespace XForms.Commands
{

    public class SearchListViewCommand : ISearchListViewCommand
    {

        public void Execute(ListViewArguments listParameter)
        {
            new XForms.Design.SearchFormTemplate().Show();
        }

        public string ID
        {
            get
            {
                return "cmd_search_list";
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
                return "Search";
            }
        }

        public string ImageName
        {
            get
            {
                return "view";
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
