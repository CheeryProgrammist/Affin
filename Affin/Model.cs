using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using Affin.Entities;

namespace Affin
{
	class Model
	{
		private int _width;
		private int _height;
		private float _nodeRadius;
		private Bitmap _canvas;
		private Bitmap _bkgnd;

		public event EventHandler<DrawingEventArgs> ModelRecalculated;

		private List<Node> _rootNodes = Node.RootNodes;
		private Node _capturedNode = default(Node);
		private Color _currentColor = Color.Blue;
		private Color _markColor = Color.IndianRed;
		private double _captureRadius = 12;

		public Model(int width = 480, int height = 480)
		{
			_width = width;
			_height = height;
			_nodeRadius = 3.5f;
			_canvas = new Bitmap(_width, _height);
			_bkgnd = new Bitmap(_width, _height);

			using (var g = Graphics.FromImage(_bkgnd))
				g.Clear(Color.White);
		}

		public void CaptureOrCreateNode(float x, float y, bool isMultiselection)
		{
			_capturedNode =
				_rootNodes.FirstOrDefault(node => Math.Sqrt(Math.Pow(node.Position.X - x, 2) + Math.Pow(node.Position.Y - y, 2)) < _captureRadius);

			if (!isMultiselection)
				foreach (var rootNode in _rootNodes)
					rootNode.IsSelected = false;

			if (_capturedNode != default(Node))
				_capturedNode.IsSelected = true;
			else
				_rootNodes.Add(new Node(x, y, _nodeRadius));

			RedrawField();
		}

		private void RedrawField()
		{
			_canvas = _bkgnd.Clone() as Bitmap;
			using (var g = Graphics.FromImage(_canvas))
			{
				foreach (var node in _rootNodes)
				{
					var x = node.Position.X - (int)node.Radius;
					var y = node.Position.Y - (int)node.Radius;
					var diameter = node.Radius * 2;
					g.DrawEllipse(new Pen(_currentColor), x, y, diameter, diameter);

					if (node.IsSelected)
						g.FillEllipse(new SolidBrush(_markColor), x + 1, y + 1, diameter - 2, diameter - 2);
				}
			}
			ModelRecalculated?.Invoke(this, new DrawingEventArgs(_canvas));
			GC.Collect();
		}

		public void MoveCapturedNode(Point newLocation)
		{
			if (_capturedNode == null)
				return;
			_capturedNode.Position.X = newLocation.X;
			_capturedNode.Position.Y = newLocation.Y;
			RedrawField();
		}

		public void ReleaseCapturedPoint()
		{
			if (_capturedNode == null)
				return;

			if (_capturedNode.Position.X < 0
				|| _capturedNode.Position.X >= _width
				|| _capturedNode.Position.Y < 0
				|| _capturedNode.Position.Y >= _height)
			{
				_rootNodes.Remove(_capturedNode);
				RedrawField();
			}

			_capturedNode = null;
		}
	}
}
