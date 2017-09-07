namespace Affin.Entities
{
	class MaterialPoint : Node
	{
		public float Mass { get; set; }

		private Vector2D _speed;
		private Vector2D _prevSpeed;

		public Vector2D Speed
		{
			get { return _speed; }
			set { _speed = value; }
		}
		
		public MaterialPoint(float x, float y, float r) : base(x, y, r)
		{
			Mass = 10;
			_prevSpeed = _speed = new Vector2D();
		}

		public void ProceedQuantumTime(double quantumDuration)
		{
			Position.X += (float) ((_prevSpeed.X + _speed.X) * quantumDuration / 2);
			Position.Y += (float) ((_prevSpeed.Y + _speed.Y) * quantumDuration / 2);
			_prevSpeed = _speed;
		}
	}

}
