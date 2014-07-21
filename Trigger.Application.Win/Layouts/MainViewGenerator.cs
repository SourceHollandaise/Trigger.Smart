using System;
using Eto.Forms;
using System.Collections.Generic;
using Eto.Drawing;

namespace Trigger.WinForms.Layout
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

		public DynamicLayout GetLayout()
		{
			DynamicLayout layout = new DynamicLayout();
		
			foreach (var type in DeclaredTypes)
			{
				var button = new Button();
				button.Text = type.Name;
				button.Tag = type;
				button.Image = ImageExtensions.GetImage("Info32.png", 32);
				button.ImagePosition = ButtonImagePosition.Left;

				button.Click += (sender, e) =>
				{
					WindowManager.ShowListView(button.Tag as Type);
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