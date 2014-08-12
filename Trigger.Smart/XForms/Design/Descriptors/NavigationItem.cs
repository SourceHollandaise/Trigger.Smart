using System;

namespace XForms.Design
{

    public class NavigationItem
    {
        public Type ModelType { get; set; }

        public int Index { get; set; }

        public string NavigationItemText { get; set; }

        public string ImageName { get; set; }

        public bool Visible { get; set; }

        public IListViewDescriptor ListView { get; set; }

        public NavigationItem(Type modelType, string navigationItemText, int index)
        {
            ModelType = modelType;
            NavigationItemText = navigationItemText;
            Index = index;
            Visible = true;
        }
    }
}
