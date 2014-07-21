using System.ComponentModel;


namespace Trigger.Datastore.Persistent
{
	public interface IPersistent
	{
		event PropertyChangedEventHandler PropertyChanged;

		object MappingId { get; set; }

		string GetRepresentation();

		void Save();

		void Delete();

		void Initialize();
	}
}
