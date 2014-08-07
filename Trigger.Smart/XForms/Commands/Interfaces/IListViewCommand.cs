
namespace XForms.Commands
{
    public interface IListViewCommand : IViewCommand
    {
        void Execute(ListViewArguments listParameter);
    }

}
