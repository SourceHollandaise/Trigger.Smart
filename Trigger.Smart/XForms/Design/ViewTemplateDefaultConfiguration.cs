using System.Collections.Generic;
using XForms.Design;
using XForms.Model;

namespace XForms.Design
{
    public class ViewTemplateDefaultConfiguration : IViewTemplateConfiguration
    {
        public bool IsCompactViewMode => false;

        public Eto.Drawing.Size ListViewCompactSize => new Eto.Drawing.Size(1024, 768);

        public Eto.Drawing.Size ListViewDefaultSize => new Eto.Drawing.Size(1024, 768);

        public Eto.Drawing.Size DetailViewCompactSize => new Eto.Drawing.Size(800, 600);

        public Eto.Drawing.Size DetailViewDefaultSize => new Eto.Drawing.Size(800, 600);

    }
    
}
