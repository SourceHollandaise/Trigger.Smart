using Eto.Drawing;

namespace Trigger.XForms.Visuals
{
    public interface IViewTemplateConfiguration
    {
        bool IsCompactViewMode { get; }

        LabelLocation LabelLocation { get; }

        Size ListViewCompactSize { get; }

        Size ListViewDefaultSize { get; }

        Size DetailViewCompactSize { get; }

        Size DetailViewDefaultSize { get; }
    }
}
