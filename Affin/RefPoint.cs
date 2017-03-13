using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Affin
{
	class RefPoint
	{
		private Point _point;

		public RefPoint(Point p)
		{
			_point = p;
		}

		public int X
		{
			get { return _point.X; }
			set { _point.X = value; }
		}

		public int Y
		{
			get { return _point.Y; }
			set { _point.Y = value; }
		}
	}
}
