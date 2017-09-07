using Affin.Entities;

namespace Affin.Effects
{
	interface IEffect
	{
		void ApplyTo(MaterialPoint mPoint, double quantumTime);
	}
}
