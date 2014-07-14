
namespace Trigger.CRM.Model
{
    public interface ISecurityInfoProvider
    {
        User CurrentUser { get; }
    }
}
