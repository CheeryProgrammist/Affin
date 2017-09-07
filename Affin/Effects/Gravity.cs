using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Affin.Entities;

namespace Affin.Effects
{
	class Gravity:IEffect
	{
		/// <summary>
		/// Acceleration (meters/sec^2)
		/// </summary>
		private Vector2D Acceleration = new Vector2D(0,0);

		public Gravity(float x, float y)
		{
			Acceleration.X = x;
			Acceleration.Y = y;
		}
		public void ApplyTo(MaterialPoint mPoint, double quantumTime)
		{
			if (mPoint == null)
				return;
			mPoint.Speed.X += (float)(Acceleration.X * quantumTime);
			mPoint.Speed.Y += (float)(Acceleration.Y * quantumTime);
		}
	}
}
