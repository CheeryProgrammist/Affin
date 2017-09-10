using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using Affin.Sprites;

namespace Affin
{
	class ViewModel
	{
		private Graphics _graph;
		private Bitmap _bkgnd;
		private Stopwatch _quantumCounter;
		private double _quantumTimeDuration;
		private Model _model;
		private static readonly object _drawLocker = new object();
		public bool IsAlive { get; set; } = false;
		public event EventHandler EndDraw;

		public ViewModel()
		{
			_quantumTimeDuration = 0;
			_quantumCounter = new Stopwatch();
			_model = new Model();
			BaseSprites.LoadSprites();
		}

		private void InitializeCanvas(int width, int height)
		{
			_bkgnd = new Bitmap(width, height);
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
			lock (_drawLocker)
			{
				_graph.Clear(Color.White);
				foreach (var node in _model.RootNodes)
				{
					var x = Convert.ToInt32(node.Position.X - (int) node.CapturingRadius);
					var y = Convert.ToInt32(node.Position.Y - (int) node.CapturingRadius);

					_graph.DrawImageUnscaled(node.IsSelected ? BaseSprites.SelectedPoint : BaseSprites.Point, x, y);
				}
				EndDraw?.Invoke(this, null);
			}
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
			var sleepTime = 1000 / 75;
			while (IsAlive)
			{
				_quantumCounter.Start();
				var task = Task.Run(() =>
				{
					_model.ProceedTime(_quantumTimeDuration);
					RedrawField();
				}, CancellationToken.None);
				Thread.Sleep(sleepTime);
				task.ContinueWith((t) =>
				{
					_quantumTimeDuration = _quantumCounter.Elapsed.TotalMilliseconds;
					_quantumCounter.Reset();
				}).Wait();
			}
		}

		public void ResizeField(int width, int height)
		{
			InitializeCanvas(width,height);
			_model.SetFieldSize(width, height);
		}

		public void SetCanvas(Graphics graph, int pbCanvasWidth, int pbCanvasHeight)
		{
			_graph = graph;
			ResizeField(pbCanvasWidth, pbCanvasHeight);
		}

		public void SelectAll()
		{
			_model.SelectAll();
			RedrawField();
		}
	}
}
