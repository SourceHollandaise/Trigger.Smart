using System.Collections.Generic;

namespace XForms.Design
{
    public class GroupItemDescription
    {
        public IList<ViewItemDescription> ViewItemDescriptions { get; set; }

        public string GroupHeaderText { get; set; }

        public int Index { get; set; }

        public bool Fill { get; set; }

        public ViewItemOrientation ViewItemOrientation { get; set; }

        public bool Visible { get; set; }

        public GroupItemDescription(string groupHeadertext, int index)
        {
            GroupHeaderText = groupHeadertext;
            Index = index;
            ViewItemOrientation = ViewItemOrientation.Vertical;
            Visible = true;
        }
    }
}
