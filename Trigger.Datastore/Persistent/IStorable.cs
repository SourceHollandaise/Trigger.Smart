using System.ComponentModel;


namespace Trigger.Datastore.Persistent
{
	public interface IStorable : INotifyPropertyChanged
	{
		object MappingId { get; set; }

		string GetRepresentation();

		void Save();

		void Delete();

		void Initialize();
	}
}
