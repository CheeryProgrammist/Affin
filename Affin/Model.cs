using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using Affin.Entities;

namespace Affin
{
	class Model
	{
		public List<Node> RootNodes
		{
			get { return _rootNodes; }
		}

		private List<Node> _rootNodes = Node.RootNodes;
		private Node _capturedNode = default(Node);
		private float _nodeRadius;
		private double _captureRadius = 12;
		private int _width;
		private int _height;
		private readonly bool _isTransparentBorders;

		public Model(int width, int height)
		{
			_isTransparentBorders = true;
			_nodeRadius = 12.5f;
			_width = width;
			_height = height;
		}

		public void ProceedTime(double quantumTime)
		{
			foreach (var rootNode in _rootNodes)
			{
				((MaterialPoint)rootNode).ProceedQuantumTime(quantumTime);
				((MaterialPoint)rootNode).Speed.X += 0.0001F;
				((MaterialPoint)rootNode).Speed.Y += 0.0001F;
				if (_isTransparentBorders)
					HandleOut();
			}
		}

		private void HandleOut()
		{
			foreach (var rootNode in RootNodes)
			{
				var outWith = _width + 2 * _nodeRadius;
				var outHeight = _height + 2 * _nodeRadius;

				while (rootNode.Position.X > outWith)
					rootNode.Position.X -= outWith;
				while (rootNode.Position.X < 0)
					rootNode.Position.X += outWith;
				while (rootNode.Position.Y > outHeight)
					rootNode.Position.Y -= outHeight;
				while (rootNode.Position.Y < 0)
					rootNode.Position.Y += outHeight;
			}
		}

		public void CaptureOrCreateNode(float x, float y, bool isMultiselection)
		{
			if(!TryCapture(x, y, isMultiselection))
				_rootNodes.Add(new MaterialPoint(x, y, _nodeRadius));
				
		}

		private bool TryCapture(float x, float y, bool isMultiselection)
		{
			_capturedNode =
				_rootNodes.FirstOrDefault(node => Math.Sqrt(Math.Pow(node.Position.X - x, 2) + Math.Pow(node.Position.Y - y, 2)) < _captureRadius);

			if (_capturedNode == null) return false;

			if (!isMultiselection)
				foreach (var rootNode in _rootNodes)
					rootNode.IsSelected = false;
			
			_capturedNode.InvertSelection();

			return true;
		}

		public void MoveCapturedNode(Point newLocation)
		{
			if (_capturedNode == null)
				return;
			_capturedNode.Position.X = newLocation.X;
			_capturedNode.Position.Y = newLocation.Y;
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
			}

			_capturedNode = null;
		}
	}
}
