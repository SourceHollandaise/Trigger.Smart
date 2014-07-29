using System.Collections.Generic;

namespace Trigger.XForms
{
    public class GroupItemDescription
    {
        public GroupItemDescription(string groupHeadertext, int index)
        {
            GroupHeaderText = groupHeadertext;
            Index = index;
        }

        public string GroupHeaderText { get; set; }

        public int Index { get; set; }

        public IList<ViewItemDescription> ViewItemDescriptions { get; set; }

        public bool Fill { get; set; }
    }
    
}
