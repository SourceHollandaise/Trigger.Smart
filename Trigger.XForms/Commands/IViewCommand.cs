
namespace Trigger.XForms.Commands
{
    public interface IViewCommand
    {
        string ID { get; }

        string Name { get; }

        string ImageName { get; }

        bool AllowExecute { get; }

        bool Visible { get; }
    }
}
