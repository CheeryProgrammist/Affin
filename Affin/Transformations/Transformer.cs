using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Affin.Transformations;

namespace Affin
{
	class Transformer
	{
		private MatrixBase _transformMatrix;

		public Transformer()
		{
			_transformMatrix = new TransferMatrix();
		}

		public IEnumerable<RefPoint> Move(IEnumerable<RefPoint> points, float xOffset, float yOffset)
		{
			var pMatrix = new PointsMatrix(points);
			var transformed = pMatrix.Multiply(_transformMatrix);
			return transformed.Rows.Select(r => new RefPoint(new Point(Convert.ToInt32(r[0]), Convert.ToInt32(r[1]))));
		}
	}
}
