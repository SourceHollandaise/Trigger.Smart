
namespace Trigger.CRM.Persistent
{
    public interface IStorable
    {
        object MappingId { get; set; }

        void Save();

        void Delete();
    }
}
