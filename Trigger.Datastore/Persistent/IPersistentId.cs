
namespace Trigger.Datastore.Persistent
{
	public interface IPersistentId
	{
		object MappingId { get; set; }

		string GetRepresentation();

		void Save();

		void Delete();

		void Initialize();
	}
}
