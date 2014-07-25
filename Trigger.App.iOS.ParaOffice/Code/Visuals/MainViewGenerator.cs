using System;
using System.Linq;
using Eto.Forms;
using System.Collections.Generic;
using Eto.Drawing;
using Trigger.XStorable.DataStore;

namespace Trigger.XForms.Visuals
{
    public class MainViewGenerator
    {
        protected IEnumerable<Type> DeclaredTypes
        {
            get;
            set;
        }

        public MainViewGenerator(IEnumerable<Type> declaredTypes)
        {
            this.DeclaredTypes = declaredTypes;
        }

        public DynamicLayout GetContent()
        {
            DynamicLayout layout = new DynamicLayout();
		
            var mainViewTypes = DeclaredTypes.Where(p => p.GetCustomAttributes(typeof(MainViewItemAttribute), true).Any());

            foreach (var type in mainViewTypes)
            {
                var displayNameAttribute = type.GetCustomAttributes(typeof(System.ComponentModel.DisplayNameAttribute), true).FirstOrDefault() as System.ComponentModel.DisplayNameAttribute;

                var button = new Button();
                button.Text = displayNameAttribute != null ? displayNameAttribute.DisplayName : type.Name;
                button.Tag = type;
                button.Image = ImageExtensions.GetImage("Info32.png", 32);
                button.ImagePosition = ButtonImagePosition.Left;

                button.Click += (sender, e) =>
                {
                    var listView = new ListViewTemplate(button.Tag as Type, null);
                    listView.Show();

                    //WindowManager.ShowListView(button.Tag as Type);
                };

                layout.BeginHorizontal();
                layout.Add(button, true);
                layout.EndHorizontal();
            }
				
            layout.BeginHorizontal();
            layout.EndHorizontal();

            return layout;
        }
    }
	
}