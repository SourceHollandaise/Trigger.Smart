
namespace XForms.Store
{
    public enum FileDataMode
    {
        None,
        FilePreview,
        SlideShow,
        MixedMode
    }

    public interface IFileData
    {
        string Subject { get; set; }

        string FileName { get; set; }
    }
}
