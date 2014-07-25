using System;
using System.Linq;
using Eto.Drawing;
using Eto.Forms;
using Trigger.XStorable.DataStore;

namespace Trigger.XForms.Visuals
{
    public class ListViewTemplate : TemplateBase
    {
        public GridView CurrentGrid
        {
            get;
            protected set;
        }

        public ListViewTemplate(Type modelType, IStorable currentObject) : base(modelType, currentObject)
        {
            if (CurrentGrid == null)
                CurrentGrid = new ListViewGenerator(ModelType).GetContent();

            Content = CurrentGrid;
            Content = new Scrollable{ Content = this.Content };

//            if (modelType.GetCustomAttributes(typeof(CompactViewRepresentationAttribute), true).FirstOrDefault() != null)
//                Size = new Size(400, 768);
//            else
//                Size = new Size(768, 400);

            var displayNameAttribute = ModelType.GetCustomAttributes(typeof(System.ComponentModel.DisplayNameAttribute), true).FirstOrDefault() as System.ComponentModel.DisplayNameAttribute;
            if (displayNameAttribute != null)
                Title = displayNameAttribute.DisplayName + " - Items: " + CurrentGrid.DataStore.AsEnumerable().Count();
            else
                Title = ModelType.Name + " - Items: " + CurrentGrid.DataStore.AsEnumerable().Count();
                
        }
    }
}
