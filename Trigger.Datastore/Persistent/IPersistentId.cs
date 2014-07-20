using System.ComponentModel;


namespace Trigger.Datastore.Persistent
{
	public interface IPersistentId
	{
		event PropertyChangedEventHandler PropertyChanged;

		object MappingId { get; set; }

		string GetRepresentation();

		void Save();

		void Delete();

		void Initialize();
	}
}
