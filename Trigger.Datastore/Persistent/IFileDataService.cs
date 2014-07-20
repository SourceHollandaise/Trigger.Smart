
namespace Trigger.Datastore.Persistent
{
	public interface IFileDataService
	{
		int LoadFromStore();

		void AddFile(IFileData fileData, string sourcePath, bool copy = true);
	}
}
