using System.Collections.Generic;
using XForms.Design;
using XForms.Model;

namespace XForms.Design
{
    public class ViewTemplateDefaultConfiguration : IViewTemplateConfiguration
    {
        public bool IsCompactViewMode
        {
            get
            {
                return false;
            }
        }

        public Eto.Drawing.Size ListViewCompactSize
        {
            get
            {
                return new Eto.Drawing.Size(1024, 768);
            }
        }

        public Eto.Drawing.Size ListViewDefaultSize
        {
            get
            {
                return new Eto.Drawing.Size(1024, 768);
            }
        }

        public Eto.Drawing.Size DetailViewCompactSize
        {
            get
            {
                return new Eto.Drawing.Size(800, 600);
            }
        }

        public Eto.Drawing.Size DetailViewDefaultSize
        {
            get
            {
                return new Eto.Drawing.Size(800, 600);
            }
        }
        
    }
    
}
