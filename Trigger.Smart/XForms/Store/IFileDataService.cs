
namespace XForms.Store
{
    public interface IFileDataService
    {
        int LoadFromStore();

        void AddFile(IFileData fileData, string sourcePath, bool copy = true);

        void OpenFile(IFileData fileData);
    }
}
