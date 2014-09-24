using XForms.Design;

namespace XForms.Commands
{

    public interface IMainViewCommand : IViewCommand
    {
        void Execute(TemplateBase template);
    }
}
