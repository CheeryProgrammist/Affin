using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using Affin.Entities;

namespace Affin
{
	class ViewModel
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
		private float _quantumTimeDuration = 100.0F / 6.0F;

		public ViewModel(int width = 480, int height = 480)
		{
			_width = width;
			_height = height;
			_nodeRadius = 12.5f;
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
				_capturedNode.InvertSelection();
			else
				_rootNodes.Add(new MaterialPoint(x, y, _nodeRadius));

			RedrawField();
		}

		private void RedrawField()
		{
			_canvas = _bkgnd.Clone() as Bitmap;
			using (var g = Graphics.FromImage(_canvas))
			{
				foreach (var node in _rootNodes)
				{
					var x = node.Position.X - (int)node.CapturingRadius;
					var y = node.Position.Y - (int)node.CapturingRadius;
					var diameter = node.CapturingRadius * 2;
					g.DrawEllipse(new Pen(_currentColor), x, y, diameter, diameter);

					if (node.IsSelected)
						g.FillEllipse(new SolidBrush(_markColor), x + 1, y + 1, diameter - 2, diameter - 2);
				}
			}
			ModelRecalculated?.Invoke(this, new DrawingEventArgs(_canvas));
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

		public void QuantumTimeLoop()
		{
			while (IsAlive)
			{
				foreach (var rootNode in _rootNodes)
				{
					((MaterialPoint)rootNode).ProceedQuantumTime(_quantumTimeDuration);
					((MaterialPoint) rootNode).Speed.X+=0.0001F;
					((MaterialPoint) rootNode).Speed.Y+=0.0001F;
				}
				RedrawField();
				Thread.Sleep((int)_quantumTimeDuration);
			}
		}

		public bool IsAlive { get; set; } = false;
	}
}
