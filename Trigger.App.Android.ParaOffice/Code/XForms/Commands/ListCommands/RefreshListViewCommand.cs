using Eto.Drawing;
using Eto.Forms;
using XForms.Design;


namespace XForms.Commands
{

    public class RefreshListViewCommand : IRefreshListViewCommand
    {
        public void Execute(ListViewArguments listParameter)
        {
            if (listParameter.Grid != null)
            {
                listParameter.Grid.ReloadList(listParameter.TargetType, listParameter.CustomDataSet);
                return;
            }

            if (listParameter.InputData is IListViewDescriptor)
            {
                var descriptor = listParameter.InputData as IListViewDescriptor;
                if (descriptor.ListDetailView)
                {
                    var builder = new ListDetailViewBuilder(descriptor, listParameter.TargetType, listParameter.CustomDataSet);
                    var content = builder.GetContent();

                    (Application.Instance.MainForm as MainViewTemplate).ContentPanel.Content = content;
                }
            }
        }

        public string ID
        {
            get
            {
                return "cmd_refresh";
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
                return "Refresh";
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
