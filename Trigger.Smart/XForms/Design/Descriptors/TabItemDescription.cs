using System.Collections.Generic;

namespace XForms.Design
{
    public class TabItemDescription
    {
        public IList<ViewItemDescription> ViewItemDescriptions { get; set; }

        public IList<GroupItemDescription> GroupItemDescriptions { get; set; }

        public string TabHeaderText { get; set; }

        public int Index { get; set; }

        public bool Visible { get; set; }

        public TabItemDescription(string tabHeadertext, int index)
        {
            TabHeaderText = tabHeadertext;
            Index = index;
            Visible = true;
        }
    }
}
