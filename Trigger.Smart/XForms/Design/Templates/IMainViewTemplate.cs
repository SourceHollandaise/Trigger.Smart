using Eto.Forms;
using System;

namespace XForms.Design
{
    public interface IMainViewTemplate
    {
        Type CurrentActiveType { get; set; }

        void SetContent(Control control);

        bool IsReduced { get; }
    }
}
