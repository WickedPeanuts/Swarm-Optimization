using Particle_Swarm_Optimization;
using Particle_Swarm_Optimization.FitnessFunction;
using PSO.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSO
{
    class GlobalParticle : AbstractParticle
    {
        public List<AbstractParticle> Swarm { get; set; }
        
        public GlobalParticle(EFunction function, EConstrictionFactor constrictionFactor, List<AbstractParticle> swarm) : base(function, constrictionFactor)
        {
            this.functionType = function;
            this.constrictionType = constrictionFactor;
            this.Swarm = swarm;
        }

        public override void UpdatePosition()
        {
            for (int i = 0; i< Parameters.DIMENSION_AMMOUNT; i++)
            {
                Position[i] += Velocity[i];
            }

            constriction.UpdateParameter();
        }

        public override void UpdateSpeed()
        {
            for (int i = 0; i < Parameters.DIMENSION_AMMOUNT; i++)
            {
                Velocity[i] = constriction.CalculateVelocity(Velocity[i], random.NextDouble(), random.NextDouble(), Position[i], PositionGBest[i], PositionPBest[i]);
            }
        }
    }
}
