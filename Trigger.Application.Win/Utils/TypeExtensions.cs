using System.Linq;
using Eto.Drawing;

namespace System
{

	public static class TypeExtensions
	{
		public static object GetDefaultPropertyValue(this object target)
		{
			var attribute = target.GetType().GetCustomAttributes(typeof(System.ComponentModel.DefaultPropertyAttribute), true).FirstOrDefault() as System.ComponentModel.DefaultPropertyAttribute;

			if (attribute != null)
			{
				var property = target.GetType().GetProperty(attribute.Name);
				if (property != null)
					return property.GetValue(target, null);
			}

			return null;

		}
	}
}

namespace Eto.Drawing
{
	public static class ImageExtensions
	{
		public static Image GetImage(string imageName, int size)
		{

			Image bitMap = Bitmap.FromResource(imageName);
			if (size > bitMap.Height)
				size = bitMap.Height;

			var image = new Bitmap(bitMap, size, size);
			bitMap.Dispose();
			return image;
		}
	}
}
