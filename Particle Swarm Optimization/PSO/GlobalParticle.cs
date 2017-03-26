using Particle_Swarm_Optimization;
using Particle_Swarm_Optimization.FitnessFunction;
using PSO.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Particle_Swarm_Optimization.PSO
{
    class GlobalParticle : AbstractParticle
    {
        public GlobalParticle(EFunction functionType, EConstrictionFactor constrictionType) : base(functionType, constrictionType)
        {
        }

        public override void UpdateSpeed()
        {
            for (int i = 0; i < Parameters.DIMENSION_AMOUNT; i++)
            {
                Velocity[i] = constriction.CalculateVelocity
                    (
                    Velocity[i], random.NextDouble(), random.NextDouble(),
                    Position[i], PositionGBest[i], PositionPBest[i]
                    );
            }
        }
    }
}
