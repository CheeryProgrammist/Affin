using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Affin
{
	public partial class MainForm : Form
	{
		private ViewModel _model;

		public MainForm()
		{
			InitializeComponent();
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			_model = new ViewModel();
			InitCanvas();
			//_model.EndDraw += (s, args) =>
			//{
			//	pbCanvas.BeginInvoke((Action) delegate
			//	{
			//		pbCanvas.Refresh();
			//	});
			//};
			_model.EndDraw += _model_EndDraw;
		}

		private void _model_EndDraw(object sender, EventArgs e)
		{
			UpdateFieldAsync();
		}

		private async void UpdateFieldAsync()
		{
			await RefreshField();
		}

		private Task RefreshField()
		{
			return Task.Run(() =>
			{
				pbCanvas.BeginInvoke((Action)delegate
				{
					pbCanvas.Refresh();
				});
			});
		}

		private void InitCanvas()
		{
			pbCanvas.Image = new Bitmap(pbCanvas.Width, pbCanvas.Height);
			_model.SetCanvas(Graphics.FromImage(pbCanvas.Image), pbCanvas.Width, pbCanvas.Height);
		}

		private void PbCanvas_MouseDown(object sender, MouseEventArgs e)
		{
			if ((e.Button & MouseButtons.Left) != 0 && !_model.IsAlive)
			{
				var isMultiselection = (ModifierKeys & Keys.Control) != 0;
				_model.CaptureOrCreateNode(e.Location.X, e.Location.Y, isMultiselection);
			}
		}

		private void pbCanvas_MouseMove(object sender, MouseEventArgs e)
		{
			if ((e.Button & MouseButtons.Left) != 0 && !_model.IsAlive)
				_model.MoveCapturedNode(e.Location);
		}

		private void pbCanvas_MouseUp(object sender, MouseEventArgs e)
		{
			if (!_model.IsAlive)
				_model.ReleaseCapturedPoint();
		}

		private void btnStart_Click(object sender, EventArgs e)
		{
			if (_model.IsAlive)
				return;
			_model.IsAlive = true;
			new TaskFactory().StartNew(() => _model.QuantumTimeLoop(), TaskCreationOptions.LongRunning);
		}

		private void btnStop_Click(object sender, EventArgs e)
		{
			_model.IsAlive = false;
		}

		private void MainForm_ResizeEnd(object sender, EventArgs e)
		{
			InitCanvas();
		}

		private void MainForm_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Control & e.KeyCode == Keys.A)
			{
				_model.SelectAll();
			}
		}
		
	}
}
