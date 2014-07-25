
namespace Trigger.XStorable.DataStore
{
	public interface IFileDataService
	{
		int LoadFromStore();

		void AddFile(IFileData fileData, string sourcePath, bool copy = true);
	}
}
