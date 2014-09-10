using Eto.Drawing;
using Eto.Forms;
using XForms.Design;


namespace XForms.Commands
{

    public class RefreshListViewCommand : IRefreshListViewCommand
    {
        public void Execute(ListViewArguments args)
        {
            if (args.Grid != null)
            {
                args.Grid.ReloadList(args.TargetType, args.CustomDataSet);
                return;
            }

            if (args.InputData is IListViewDescriptor)
            {
                var descriptor = args.InputData as IListViewDescriptor;
                if (descriptor.ListDetailView)
                {
                    var builder = new ListDetailViewBuilder(descriptor, args.TargetType, args.CustomDataSet);
                    var content = builder.GetContent();

                    /*
                    var scrollable = new Scrollable()
                    {
                        Border = BorderType.None,
                        Size = new Size(-1, -1),
                        Content = content
                    };
                    */

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
