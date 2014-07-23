using Trigger.XStore.Security;


namespace Trigger.XStore.Security
{
    public interface ISecurityInfoProvider
    {
        User CurrentUser { get; }
    }
}
