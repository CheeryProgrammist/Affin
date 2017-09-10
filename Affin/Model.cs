using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using Affin.Effects;
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
		private List<IEffect> _effects = new List<IEffect>();
		private Node _capturedNode = default(Node);
		private float _nodeRadius = 12.5f;
		private double _captureRadius = 12;
		private int _width;
		private int _height;
		private readonly bool _isTransparentBorders = true;
		private static readonly float _timeScale = (float)(1.0/128.0);
		private static readonly Random _rnd = new Random((int)DateTime.Now.TimeOfDay.TotalMinutes);

		public Model()
		{
			LoadEffects();
		}

		private void LoadEffects()
		{
			_effects.Add(new Gravity(0, 9.8f));
		}

		public void ProceedTime(double quantumTime)
		{
			quantumTime *= _timeScale;
			foreach (var rootNode in _rootNodes)
			{
				foreach (var effect in _effects)
					effect.ApplyTo(rootNode as MaterialPoint, quantumTime);
				var point = rootNode as MaterialPoint;
				if (point == null)
					continue;
				point.ProceedQuantumTime(quantumTime);

				var bottom = point.Position.Y + _nodeRadius + 3;
				if (bottom > _height)
				{
					point.Position.Y -= bottom - _height;
					point.Speed.Y = -point.Speed.Y;
				}
			}
			if (_isTransparentBorders)
			{
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
			if (!TryCapture(x, y, isMultiselection))
			{
				var point = new MaterialPoint(x, y, _nodeRadius);
				point.Speed.X = _rnd.Next(200) - 100;
				_rootNodes.Add(point);
			}
				
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

		public void SetFieldSize(int width, int height)
		{
			_width = width;
			_height = height;
		}

		public void SelectAll()
		{
			foreach (var node in _rootNodes)
			{
				node.IsSelected = true;
			}
		}
	}
}
