using System;
using System.Collections.Generic;

namespace Affin.Entities
{
	class Node
	{
		public static List<Node> RootNodes { get; set; } = new List<Node>();
		public float CapturingRadius { get; set; }

		public bool IsSelected { get; set; }

		public RefPoint Position => _position;
		private RefPoint _position;

		public Node(float x, float y, float r)
		{
			_position = new RefPoint((int)Math.Round(x), (int)Math.Round(y));
			CapturingRadius = r;
			IsSelected = true;
		}

		public void InvertSelection()
		{
			IsSelected = !IsSelected;
		}
	}
}
