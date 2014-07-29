using Trigger.XForms.Visuals;
using Eto.Drawing;

namespace Trigger.XForms.Visuals
{

    public class ViewTemplateConfiguration : IViewTemplateConfiguration
    {
        public bool IsCompactViewMode
        {
            get
            {
                return true;
            }
        }

        public LabelLocation LabelLocation
        {
            get
            {
                return LabelLocation.AboveControl;
            }
        }

        public Size ListViewCompactSize
        {
            get
            {
                return new Size(-1, -1);
            }
        }

        public Size ListViewDefaultSize
        {
            get
            {
                return new Size(800, 680);
            }
        }

        public Size DetailViewCompactSize
        {
            get
            {
                return new Size(-1, -1);
            }
        }

        public Size DetailViewDefaultSize
        {
            get
            {
                return new Size(680, 680);
            }
        }

    }
}
