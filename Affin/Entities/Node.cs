using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Affin.Entities
{
	class Node
	{
		private RefPoint _position;

		public Node(float x, float y)
		{
			_position = new RefPoint((int)Math.Round(x), (int)Math.Round(y));
		}
	}
}
