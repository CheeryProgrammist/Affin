using System;
using System.Drawing;
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
			pbCanvas.Image = new Bitmap(pbCanvas.Width, pbCanvas.Height);
			_model = new ViewModel(Graphics.FromImage(pbCanvas.Image) ,pbCanvas.Width, pbCanvas.Height);
			_model.EndDraw += (s, args) =>
			{
				pbCanvas.BeginInvoke((Action) delegate
				{
					pbCanvas.Refresh();
				});
			};
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
			Task.Run(() => _model.QuantumTimeLoop());
		}

		private void btnStop_Click(object sender, EventArgs e)
		{
			_model.IsAlive = false;
		}
	}
}
