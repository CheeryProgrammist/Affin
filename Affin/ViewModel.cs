using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;

namespace Affin
{
	class ViewModel
	{
		private readonly Graphics _graph;
		private Bitmap _canvas;
		private Bitmap _bkgnd;
		private Stopwatch _quantumCounter;
		
		private Pen _currentPen;
		private SolidBrush _markBrush;
		private double _quantumTimeDuration;
		private Model _model;
		public bool IsAlive { get; set; } = false;
		public event EventHandler EndDraw;

		public ViewModel(Graphics graph, int width = 480, int height = 480)
		{
			_model = new Model(width, height);
			_quantumTimeDuration = 0;
			_graph = graph;
			_canvas = new Bitmap(width, height);
			_bkgnd = new Bitmap(width, height);
			_currentPen = new Pen(Color.Blue);
			_markBrush = new SolidBrush(Color.IndianRed);
			_quantumCounter = new Stopwatch();
			using (var g = Graphics.FromImage(_bkgnd))
				g.Clear(Color.White);
		}

		public void CaptureOrCreateNode(float x, float y, bool isMultiselection)
		{
			_model.CaptureOrCreateNode(x, y, isMultiselection);
			RedrawField();
		}

		private void RedrawField()
		{
			_graph.Clear(Color.White);
			foreach (var node in _model.RootNodes)
			{
				var x = node.Position.X - (int) node.CapturingRadius;
				var y = node.Position.Y - (int) node.CapturingRadius;
				var diameter = node.CapturingRadius * 2;
				_graph.DrawEllipse(_currentPen, x, y, diameter, diameter);

				if (node.IsSelected)
					_graph.FillEllipse(_markBrush, x + 1, y + 1, diameter - 2, diameter - 2);

			}
			EndDraw?.Invoke(this, null);
		}

		public void MoveCapturedNode(Point newLocation)
		{
			_model.MoveCapturedNode(newLocation);
			RedrawField();
		}

		public void ReleaseCapturedPoint()
		{
			_model.ReleaseCapturedPoint();
			RedrawField();
		}

		public void QuantumTimeLoop()
		{
			while (IsAlive)
			{
				_quantumCounter.Start();
				var task = Task.Run(() =>
				{
					_model.ProceedTime(_quantumTimeDuration);
					RedrawField();
				});
				Thread.Sleep(10);
				task.Wait();
				_quantumTimeDuration = _quantumCounter.Elapsed.TotalMilliseconds;
				_quantumCounter.Reset();
			}
		}

	}
}
