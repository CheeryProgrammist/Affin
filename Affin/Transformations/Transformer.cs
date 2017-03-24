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
			return transformed.Rows.Select(r => new RefPoint(r[0], r[1]));
		}
	}
}
