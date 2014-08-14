using System;
using XForms.Commands;

namespace XForms.Design
{

    public class NavigationItemDescription
    {
        public Type ModelType { get; set; }

        public int Index { get; set; }

        public string NavigationItemText { get; set; }

        public string ImageName { get; set; }

        public bool Visible { get; set; }

        public IListViewDescriptor ListView { get; set; }

        public ButtonDisplayStyle DisplayStyle { get; set; }

        public NavigationItemDescription(Type modelType, string navigationItemText, int index)
        {
            ModelType = modelType;
            NavigationItemText = navigationItemText;
            Index = index;
            Visible = true;
            DisplayStyle = ButtonDisplayStyle.ImageAndText;
        }
    }
}
