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
		private Point p;
		private float _x;
		private float _y;

		public RefPoint(Point p)
		{
			this.p = p;
		}

		public RefPoint(float x, float y)
		{
			_x = x;
			_y = y;
		}

		public float X
		{
			get { return _x; }
			set { _x = value; }
		}

		public float Y
		{
			get { return _y; }
			set { _y = value; }
		}
	}
}
