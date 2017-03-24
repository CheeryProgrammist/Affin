using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Affin
{
	class Model
	{
		private int _width;
		private int _height;
		private Bitmap _canvas;
		private Bitmap _bkgnd;

		public event EventHandler<DrawingEventArgs> ModelRecalculated;

		private List<RefPoint> _points = new List<RefPoint>();
		private RefPoint _capturedPoint = default(RefPoint);
		private Color _currentColor = Color.Blue;
		private double _captureRadius = 12;

		public Model(int width = 480, int height = 480)
		{
			_width = width;
			_height = height;
			_canvas = new Bitmap(_width, _height);
			_bkgnd = new Bitmap(_width, _height);

			using (var g = Graphics.FromImage(_bkgnd))
				g.Clear(Color.White);
		}

		public void CaptureOrCreatePoint(float x, float y)
		{
			var point =
				_points.FirstOrDefault(pt => Math.Sqrt(Math.Pow(pt.X - x, 2) + Math.Pow(pt.Y - y, 2)) < _captureRadius);

			if (point != default(RefPoint))
			{
				_capturedPoint = point;
				return;
			}

			_points.Add(new RefPoint(x, y));
			RedrawField();
		}

		private void RedrawField()
		{
			_canvas = _bkgnd.Clone() as Bitmap;
			using (var g = Graphics.FromImage(_canvas))
			{
				foreach (var p in _points)
					g.DrawEllipse(new Pen(_currentColor), p.X - 3, p.Y - 3, 7, 7);
			}
			ModelRecalculated?.Invoke(this, new DrawingEventArgs(_canvas));
			GC.Collect();
		}

		public void MoveCapturedPoint(Point newLocation)
		{
			if (_capturedPoint == null)
				return;
			_capturedPoint.X = newLocation.X;
			_capturedPoint.Y = newLocation.Y;
			RedrawField();
		}

		public void ReleaseCapturedPoint()
		{
			if(_capturedPoint == null)
				return;

			if (_capturedPoint.X < 0
			    || _capturedPoint.X > _width
			    || _capturedPoint.Y < 0
			    || _capturedPoint.Y > _height)
			{
				_points.Remove(_capturedPoint);
				RedrawField();
			}

			_capturedPoint = null;
		}
	}
}
