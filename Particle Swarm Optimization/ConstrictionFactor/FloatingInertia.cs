using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Particle_Swarm_Optimization.ConstrictionFactor
{
    public class FloatingInertia : AbstractConstrictionFactor
    {
        private static readonly double InertiaFactor = 0.5 / Parameters.ITERATION_AMOUNT;
        private double Inertia = 0.9d;

        public override double CalculateVelocity(double velocity, double random1, double random2, double position, double positionGlobalBest, double positionPersonalBest)
        {
            return base.CalculateVelocity(Inertia, velocity, random1, random2, position, positionGlobalBest, positionPersonalBest);
        }

        public override void UpdateParameter()
        {
            Inertia -= InertiaFactor;
        }
    }
}
