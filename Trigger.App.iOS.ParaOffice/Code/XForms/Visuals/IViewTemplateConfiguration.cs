using Eto.Drawing;

namespace Trigger.XForms.Visuals
{
    public enum LabelControlLocation
    {
        AboveControl,
        BeforeControl,
        None
    }

    public interface IViewTemplateConfiguration
    {
        bool IsCompactViewMode { get; }

        LabelControlLocation LabelLocation { get; }

        Size ListViewCompactSize { get; }

        Size ListViewDefaultSize { get; }

        Size DetailViewCompactSize { get; }

        Size DetailViewDefaultSize { get; }
    }
}
