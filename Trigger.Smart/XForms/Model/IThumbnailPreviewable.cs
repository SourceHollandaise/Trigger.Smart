using XForms.Dependency;
using XForms.Security;
using Eto.Drawing;

namespace XForms.Model
{

    public interface IThumbnailPreviewable
    {
        Image Thumbnail { get; }
    }
}
