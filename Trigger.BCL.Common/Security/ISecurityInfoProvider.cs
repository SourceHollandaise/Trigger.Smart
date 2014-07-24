using Trigger.BCL.Common.Model;


namespace Trigger.BCL.Common.Security
{
    public interface ISecurityInfoProvider
    {
        User CurrentUser { get; }

        void SetUser(User user);
    }
}
