using Trigger.CRM.Persistent;

namespace Trigger.CRM.Model
{
    public interface IUser : ILogonParameters, IStorable
    {
        string EMail
        {
            get;
            set;
        }
    }
    
}
