
namespace XForms.Store
{
    public interface IFileDataService
    {
        int LoadFromStore();

        void StoreFile(string sourcePath, bool copy = true);

        void AddFile(IFileData fileData, string sourcePath, bool copy = true);

        void OpenFile(IFileData fileData);
    }
}
