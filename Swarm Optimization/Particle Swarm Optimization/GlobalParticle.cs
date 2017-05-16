using Particle_Swarm_Optimization;
using Particle_Swarm_Optimization.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Particle_Swarm_Optimization.Common;
using Swarm_Optimization.FitnessFunction.Enum;

namespace Particle_Swarm_Optimization.Particle_Swarm_Optimization
{
    class GlobalParticle : AbstractPSOParticle
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
