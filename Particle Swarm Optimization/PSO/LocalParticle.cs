﻿using PSO;
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
        private List<AbstractParticle> neighbors;

        public LocalParticle(EFunction functionType, EConstrictionFactor parameter) : base(functionType, parameter)
        {
        }

        public void LinkSwarm(List<AbstractParticle> swarm, int index)
        {
            neighbors = new List<AbstractParticle>();
            neighbors.Add(this);
            neighbors.Add(swarm[(index > 0 ? index - 1 : swarm.Count - 1)]);
            neighbors.Add(swarm[(index + 1 < swarm.Count ? index + 1 : 0)]);
        }

        public override void UpdateSpeed()
        {
            //Selecionar partícula mais "relevante"
            AbstractParticle bestParticle = neighbors.OrderBy(x => x.PersonalBest).First();

            //Fazer as matemágicas
            for (int i = 0; i<Parameters.DIMENSION_AMOUNT; i++)
            {
                Velocity[i] = constriction.CalculateVelocity
                    (
                    Velocity[i], random.NextDouble(), random.NextDouble(), 
                    Position[i], bestParticle.PositionPBest[i], PositionPBest[i]
                    );
            }
        }
    }
}
