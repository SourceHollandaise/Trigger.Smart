using Trigger.BCL.Common.Model;


namespace Trigger.BCL.Common.Security
{
    public interface ISecurityInfoProvider
    {
        string UserName { get; set; }

        User CurrentUser { get; }
    }
}
