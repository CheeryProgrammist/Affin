namespace Affin.Entities
{
	class MaterialPoint : Node
	{
		public float Mass { get; set; }

		private Vector2D _speed;

		public Vector2D Speed
		{
			get { return _speed; }
			set { _speed = value; }
		}
		
		public MaterialPoint(float x, float y, float r) : base(x, y, r)
		{
			Mass = 10;
			_speed = new Vector2D();
		}

		public void ProceedQuantumTime(double quantumDuration)
		{
			Position.X += (float) (_speed.X * quantumDuration);
			Position.Y += (float) (_speed.Y * quantumDuration);
		}
	}

}
