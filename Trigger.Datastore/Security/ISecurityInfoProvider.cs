using Trigger.Datastore.Security;


namespace Trigger.Datastore.Security
{
    public interface ISecurityInfoProvider
    {
        User CurrentUser { get; }
    }
}
