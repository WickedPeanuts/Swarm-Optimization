using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Particle_Swarm_Optimization.Constriction_Factor
{
    class FixedInertia : AbstractConstrictionFactor
    {
        public static readonly double Inertia = 0.8d;

        public override double CalculateVelocity(double velocity, double random1, double random2, double position, double positionGlobalBest, double positionPersonalBest)
        {
            return base.CalculateVelocity(Inertia, velocity, random1, random2, position, positionGlobalBest, positionPersonalBest);
        }
    }
}
