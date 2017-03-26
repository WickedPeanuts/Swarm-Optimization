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

        public override void UpdatePosition()
        {
            for (int i = 0; i < Parameters.DIMENSION_AMOUNT; i++)
            {
                Position[i] += Velocity[i];
            }

            constriction.UpdateParameter();
        }

        public override void UpdateSpeed()
        {
            //Selecting Adjacent Particles
            Dictionary<int, double> adjacentParticlesDistance = new Dictionary<int, double>();

            //Add Particles to dictionary
            for(int i = 0; i < Parameters.PARTICLE_AMOUNT; i++)
            {
                if (swarm[i] != this)
                {
                    adjacentParticlesDistance.Add(i, AbstractFunction.EuclidianDistance(swarm[i].Position, Position));
                }
            }

            //Select N closest particles
            AbstractParticle bestParticle = null;

            for (int i = 0; i < Parameters.FOCAL_ADJACENT_PARTICLES; i++)
            {
                double lesserDistance = adjacentParticlesDistance.First(x => x.Key >= 0).Value;
                int position = 0;
                foreach(KeyValuePair<int, double> key in adjacentParticlesDistance)
                {
                    if (lesserDistance < adjacentParticlesDistance[key.Key])
                    {
                        lesserDistance = adjacentParticlesDistance[key.Key];
                        position = key.Key;
                    }
                }

                //Select the best particle
                if (bestParticle != null)
                {
                    if (bestParticle.PersonalBest < swarm[position].PersonalBest)
                        bestParticle = swarm[position];
                }
                else
                {
                    bestParticle = swarm[position];
                }
            }

            //Verificar se bestParticle tem menos fitness
            if (bestParticle.PersonalBest < this.PersonalBest)
                bestParticle = this;

            //Do the maths
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
