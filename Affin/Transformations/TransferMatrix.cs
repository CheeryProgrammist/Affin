using Affin.Transformations;

namespace Affin
{
	internal class TransferMatrix : MatrixBase
	{
		public TransferMatrix() : base(GenerateTransferArray()) { }

		private static float[,] GenerateTransferArray()
		{
			var m = new float[3, 3];
			for (int i = 0; i < 3; i++)
				m[i, i] = 1;
			return m;
		}
	}
}