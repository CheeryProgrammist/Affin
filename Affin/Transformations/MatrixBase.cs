using System.Collections.Generic;
using System.Linq;

namespace Affin.Transformations
{
	class MatrixBase
	{
		private float[,] _matrix;
		private int _rowCount;
		private int _colCount;

		public IEnumerable<float[]> Rows
		{
			get
			{
				for (int rowIndex = 0; rowIndex < _rowCount; rowIndex++)
					yield return GetRowByIndex(rowIndex);
			}
		}

		private IEnumerable<float[]> Columns
		{
			get
			{
				for (int colIndex = 0; colIndex < _colCount; colIndex++)
					yield return GetColumnByIndex(colIndex);
			}
		}

		protected MatrixBase(float[,] matrix)
		{
			_matrix = matrix;
			_rowCount = _matrix.GetLength(0);
			_colCount = _matrix.GetLength(1);
		}

		public virtual MatrixBase Multiply(MatrixBase other)
		{
			if (!IsValidMatrix(_matrix) || !IsValidMatrix(other._matrix)
				|| _colCount != other._rowCount)
				return null;

			var resultMatrix = new float[_rowCount, other._colCount];

			for (int rowIndex = 0; rowIndex < _rowCount; rowIndex++)
				for (int colIndex = 0; colIndex < other._colCount; colIndex++)
					resultMatrix[rowIndex, colIndex] = GetRowByIndex(rowIndex).Zip(GetColumnByIndex(colIndex), (x, y) => x * y).Sum();

			return new MatrixBase(resultMatrix);
		}

		private static bool IsValidMatrix(float[,] m)
		{
			return m != null
			       && m.GetLength(0) >= 1
				   && m.GetLength(1) > 1;
		}

		private float[] GetRowByIndex(int index)
		{
			var row = new float[_colCount];
			for (int i = 0; i < _colCount; i++)
				row[i] = _matrix[index, i];
			return row;
		}

		private float[] GetColumnByIndex(int index)
		{
			var col = new float[_rowCount];
			for (int i = 0; i < _rowCount; i++)
				col[i] = _matrix[i, index];
			return col;
		}
	}
}
