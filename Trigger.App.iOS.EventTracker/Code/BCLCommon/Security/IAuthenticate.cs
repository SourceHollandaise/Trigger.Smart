
namespace Trigger.BCL.Common.Security
{
    public interface IAuthenticate
    {
        bool LogOn(LogonParameters logonParameters);
    }
}
