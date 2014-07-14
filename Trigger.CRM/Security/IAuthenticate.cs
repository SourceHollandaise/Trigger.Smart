
namespace Trigger.CRM.Model
{
    public interface IAuthenticate
    {
        bool LogOn(LogonParameters logonparamters);
    }
}
