using XForms.Commands;

namespace Trigger.BCL.ParaOffice
{

    public class RueckverkehrAbholenListViewCommand : IRueckverkehrAbholenListViewCommand
    {

        public void Execute(ListViewArguments listParameter)
        {

        }

        public string ID
        {
            get
            {
                return "cmd_erv_rueckverkehr_abholen";
            }
        }

        public string Name
        {
            get
            {
                return "Rückverkehr abholen";
            }
        }

        public string ImageName
        {
            get
            {
                return "cloud_computing_refresh";
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

        public ButtonDisplayStyle DisplayStyle
        {
            get
            {
                return ButtonDisplayStyle.Text;
            }
        }
    }
}
