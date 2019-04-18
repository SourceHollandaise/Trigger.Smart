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

                    (Application.Instance.MainForm as IMainViewTemplate).SetContent(content);
                }
            }
        }

        public string ID => "cmd_refresh";

        public ButtonDisplayStyle DisplayStyle => ButtonDisplayStyle.Image;

        public string Name => "Refresh";

        public string ImageName => "cloud_computing_refresh";

        public int Width => 34;

        public bool AllowExecute => true;

        public bool Visible => true;
    }
}
