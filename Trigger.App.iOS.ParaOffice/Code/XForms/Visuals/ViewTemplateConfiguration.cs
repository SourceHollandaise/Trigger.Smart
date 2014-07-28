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

        public Size ListViewCompactSize
        {
            get
            {
                return new Size(480, 800);
            }
        }

        public Size ListViewDefaultSize
        {
            get
            {
                return new Size(800, 480);
            }
        }

        public Size DetailViewCompactSize
        {
            get
            {
                return new Size(480, 800);
            }
        }

        public Size DetailViewDefaultSize
        {
            get
            {
                return new Size(800, 480);
            }
        }

    }
}
