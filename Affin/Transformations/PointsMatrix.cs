using System.Collections.Generic;
using System.Linq;

namespace Affin.Transformations
{
	class PointsMatrix : MatrixBase
	{
		public PointsMatrix(IEnumerable<RefPoint> points) : base(ConvertToArray(points))
		{
		}

		private static float[,] ConvertToArray(IEnumerable<RefPoint> points)
		{
			var pointsArray = new float[points.Count(), 3];
			int pointIndex = 0;

			foreach (var point in points)
			{
				pointsArray[pointIndex, 0] = point.X;
				pointsArray[pointIndex, 1] = point.Y;
				pointsArray[pointIndex++, 2] = 1;
			}

			return pointsArray;
		}
	}
}
