using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Affin.Sprites
{
	class BaseSprites
	{
		public static Bitmap Point { get; private set; }
		public static Bitmap SelectedPoint { get; private set; }
		private static Pen _bluePen = new Pen(Color.Blue);
		private static Brush _indianBrush = new SolidBrush(Color.IndianRed);
		public static void LoadSprites()
		{
			LoadPoint();
			LoadSelectedPoint();
		}

		private static void LoadPoint()
		{
			Point = new Bitmap(26, 26);
			using (var g = Graphics.FromImage(Point))
			{
				g.DrawEllipse(_bluePen, 0,0,25,25);
			}
		}

		private static void LoadSelectedPoint()
		{
			SelectedPoint = new Bitmap(26, 26);
			using (var g = Graphics.FromImage(SelectedPoint))
			{
				g.DrawEllipse(_bluePen, 0, 0, 25, 25);
				g.FillEllipse(_indianBrush, 1, 1, 23, 23);
			}
		}
	}
}
