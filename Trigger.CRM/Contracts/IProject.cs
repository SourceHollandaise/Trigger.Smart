
using Trigger.CRM.Persistent;

namespace Trigger.CRM.Model
{
    public interface IProject : IStorable
    {
        string Name
        {
            get;
            set;
        }

        string Description
        {
            get;
            set;
        }
    }
    
}
