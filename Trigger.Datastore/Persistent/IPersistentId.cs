using Trigger.Datastore.Security;
using System.IO;
using Trigger.CRM.Persistent;


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
