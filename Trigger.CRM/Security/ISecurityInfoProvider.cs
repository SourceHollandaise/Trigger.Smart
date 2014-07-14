using Trigger.CRM.Model;


namespace Trigger.CRM.Security
{
    public interface ISecurityInfoProvider
    {
        User CurrentUser { get; }
    }
}
