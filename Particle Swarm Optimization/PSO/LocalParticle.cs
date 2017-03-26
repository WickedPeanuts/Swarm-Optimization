using PSO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PSO.Enum;
using Particle_Swarm_Optimization.FitnessFunction;

namespace Particle_Swarm_Optimization.PSO
{
    class LocalParticle : AbstractParticle
    {
        private List<AbstractParticle> swarm;

        public LocalParticle(EFunction functionType, EConstrictionFactor parameter, List<AbstractParticle> swarm) : base(functionType, parameter)
        {
            this.swarm = swarm;
        }

        public override void UpdateSpeed()
        {
            //Ordenar enxame por distancia
            swarm.OrderBy(x => AbstractFunction.EuclidianDistance(this.Position, x.Position));
            AbstractParticle bestParticle = swarm[0];

            //Selecionar partícula mais "relevante"
            for (int i = 2; i < Parameters.FOCAL_ADJACENT_PARTICLES; i++)
            {
                if (swarm[i].PersonalBest < bestParticle.PersonalBest)
                {
                    bestParticle = swarm[i];
                }
            }

            //Fazer as matemágicas
            for (int i = 0; i<Parameters.DIMENSION_AMOUNT; i++)
            {
                Velocity[i] = constriction.CalculateVelocity
                    (
                    Velocity[i], random.NextDouble(), random.NextDouble(), 
                    Position[i], PositionGBest[i], bestParticle.Position[i]
                    );
            }
        }
    }
}
