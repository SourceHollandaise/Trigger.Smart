using Trigger.XStorable.Model;


namespace Trigger.XStorable.Security
{
    public interface ISecurityInfoProvider
    {
        User CurrentUser { get; }
    }
}
