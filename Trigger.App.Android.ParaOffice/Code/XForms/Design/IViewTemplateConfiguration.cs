using Eto.Drawing;

namespace XForms.Design
{
    public interface IViewTemplateConfiguration
    {
        bool IsCompactViewMode { get; }

        Size ListViewCompactSize { get; }

        Size ListViewDefaultSize { get; }

        Size DetailViewCompactSize { get; }

        Size DetailViewDefaultSize { get; }
    }
}
