﻿using System;
using System.Windows.Forms;

namespace Affin
{
	public partial class MainForm : Form
	{
		private Model _model;
		public MainForm()
		{
			InitializeComponent();
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			_model = new Model(pbCanvas.Width, pbCanvas.Height);
			_model.ModelRecalculated += Model_ModelRecalculated;
		}

		private void Model_ModelRecalculated(object sender, DrawingEventArgs e)
		{
			pbCanvas.Image = e.Canvas;
		}

		private void PbCanvas_MouseDown(object sender, MouseEventArgs e)
		{
			if ((e.Button & MouseButtons.Left) != 0)
			{
				var isMultiselection = (ModifierKeys & Keys.Control) != 0;
				_model.CaptureOrCreateNode(e.Location.X, e.Location.Y, isMultiselection);
			}
		}

		private void pbCanvas_MouseMove(object sender, MouseEventArgs e)
		{
			if ((e.Button & MouseButtons.Left) != 0)
				_model.MoveCapturedNode(e.Location);
		}

		private void pbCanvas_MouseUp(object sender, MouseEventArgs e)
		{
			_model.ReleaseCapturedPoint();
		}
	}
}
