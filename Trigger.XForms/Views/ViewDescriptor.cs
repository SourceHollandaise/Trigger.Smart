using System.Collections.Generic;

namespace Trigger.XForms
{
    public abstract class ViewDescriptor
    {
        public IList<GroupItemDescription> GroupItemDescriptions { get; set; }

        public IList<TabItemDescription> TabItemDescriptions { get; set; }
    }

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

    public class ViewItemDescription
    {
        public ViewItemDescription(string fieldName, int index)
        {
            FieldName = fieldName;
            Index = index;
            LabelOrientation = LabelOrientation.Left;
        }

        public string FieldName { get; set; }

        public string LabelText { get; set; }

        public bool ShowLabel { get; set; }

        public LabelOrientation LabelOrientation { get; set; }

        public int Index { get; set; }

        public bool Fill { get; set; }
    }

    public enum LabelOrientation
    {
        Left,
        Right,
        Top,
        Bottom
    }
}

