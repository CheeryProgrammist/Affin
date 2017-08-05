namespace Affin.Entities
{
	internal class Vector2D
	{
		public float X { get; set; }
		public float Y { get; set; }

		public Vector2D() : this(0, 0) { }

		public Vector2D(float x, float y)
		{
			X = x;
			Y = y;
		}

		public static Vector2D operator +(Vector2D lhs, Vector2D rhs)
		{
			return new Vector2D(lhs.X + rhs.X, lhs.Y + rhs.Y);
		}
	}
}
