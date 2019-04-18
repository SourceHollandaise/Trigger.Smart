
namespace XForms.Commands
{

    public class SaveObjectDetailViewCommand : ISaveObjectDetailViewCommand
    {
        public void Execute(DetailViewArguments args)
        {
            if (args.CurrentObject != null)
                args.CurrentObject.Save();
        }

        public string ID => "cmd_save";

        public ButtonDisplayStyle DisplayStyle => ButtonDisplayStyle.Image;

        public string Name => "Save";

        public string ImageName => "floppy_disk";

        public int Width => 34;

        public bool AllowExecute => true;

        public bool Visible => true;
    }
}
