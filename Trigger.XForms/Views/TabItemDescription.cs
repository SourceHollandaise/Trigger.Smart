using System.Collections.Generic;

namespace Trigger.XForms
{
    public class TabItemDescription
    {
        public TabItemDescription(string tabHeadertext, int index)
        {
            TabHeaderText = tabHeadertext;
            Index = index;
        }

        public string TabHeaderText { get; set; }

        public int Index { get; set; }

        public IList<ViewItemDescription> ViewItemDescriptions { get; set; }

        public IList<GroupItemDescription> GroupItemDescriptions { get; set; }
    }
}
