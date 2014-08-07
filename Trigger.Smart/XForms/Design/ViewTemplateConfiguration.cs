using Eto.Drawing;

namespace XForms.Design
{

    public class ViewTemplateConfiguration : IViewTemplateConfiguration
    {
        public bool IsCompactViewMode
        {
            get
            {
                return false;
            }
        }

        public Size ListViewCompactSize
        {
            get
            {
                return new Size(480, 680);
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
                return new Size(480, 680);
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
