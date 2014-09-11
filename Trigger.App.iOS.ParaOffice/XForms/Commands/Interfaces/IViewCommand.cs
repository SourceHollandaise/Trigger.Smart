
namespace XForms.Commands
{
    public interface IViewCommand
    {
        string ID { get; }

        string Name { get; }

        string ImageName { get; }

        int Width { get; }

        bool AllowExecute { get; }

        bool Visible { get; }

        ButtonDisplayStyle DisplayStyle { get; }
    }

    public enum ButtonDisplayStyle
    {
        Image,
        Text,
        ImageAndText
    }
}
