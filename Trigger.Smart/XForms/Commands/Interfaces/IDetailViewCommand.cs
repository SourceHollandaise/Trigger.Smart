
namespace XForms.Commands
{
    public interface IDetailViewCommand : IViewCommand
    {
        void Execute(DetailViewArguments args);
    }
}
