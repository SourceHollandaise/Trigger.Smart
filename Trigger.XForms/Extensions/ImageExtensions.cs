using System.Linq;
using Eto.Drawing;

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
