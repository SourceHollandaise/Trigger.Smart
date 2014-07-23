
namespace Trigger.XForms.Security
{
    public interface ISecurityInfoProvider
    {
        User CurrentUser { get; }
    }
}
