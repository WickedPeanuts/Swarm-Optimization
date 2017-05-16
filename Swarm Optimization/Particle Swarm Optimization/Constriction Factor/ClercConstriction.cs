using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Particle_Swarm_Optimization.Constriction_Factor
{
    public class ClercConstriction : AbstractConstrictionFactor
    {
        /* K = 2/(2 - Phi - Sqrt(Phi^2 - 4Phi))
         */
        private static readonly double Phi = C1 + C2;
        private static readonly double Constriction = 2 / Math.Abs((2 - Phi - Math.Sqrt(Phi * Phi - 4 * Phi)));
        
        public override double CalculateVelocity(double velocity, double random1, double random2, double position, double positionGlobalBest, double positionPersonalBest)
        {
            return (Constriction * base.CalculateVelocity(1, velocity, random1, random2, position, positionGlobalBest, positionPersonalBest));
        }
    }
}
