using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Affin
{
	class DrawingEventArgs : EventArgs
	{
		public Image Canvas { get; }

		public DrawingEventArgs(Image image)
		{
			Canvas = image;
		}
	}
}
