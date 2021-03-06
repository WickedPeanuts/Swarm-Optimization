﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Particle_Swarm_Optimization.Enum;
using Particle_Swarm_Optimization.Common;
using Swarm_Optimization.FitnessFunction.Enum;

namespace Particle_Swarm_Optimization.Particle_Swarm_Optimization
{
    public class FocalParticle : AbstractPSOParticle
    {
        public bool IsFocalParticle { get; set; }

        private List<AbstractPSOParticle> swarm;
        private AbstractPSOParticle focalParticle;
        
        public FocalParticle(EFunction functionType, EConstrictionFactor constrictionType, List<AbstractPSOParticle> swarm,  bool isFocalParticle) : base(functionType, constrictionType)
        {
            this.swarm = swarm;
            this.IsFocalParticle = isFocalParticle;
            
            foreach(AbstractPSOParticle abs in swarm)
            {
                if (((FocalParticle)abs).IsFocalParticle)
                {
                    focalParticle = abs;
                    break;
                }
            }
        }

        public override void UpdateSpeed()
        {
            //FocalParticle seleciona a particula mais "influente"
            AbstractPSOParticle bestParticle = null;

            if (IsFocalParticle)
            {
                bestParticle = swarm[0];

                for (int j = 0; j < Parameters.PARTICLE_AMOUNT; j++)
                {
                    if (swarm[j].PersonalBest < bestParticle.PersonalBest)
                    {
                        bestParticle = swarm[j];
                    }
                }
            }

            for (int i = 0; i<Parameters.DIMENSION_AMOUNT; i++)
            {
                if (IsFocalParticle)
                {
                    Velocity[i] = constriction.CalculateVelocity
                        (
                        Velocity[i], random.NextDouble(), random.NextDouble(),
                        Position[i], PositionPBest[i], bestParticle.PositionPBest[i]
                        );
                }
                else
                {
                    Velocity[i] = constriction.CalculateVelocity
                        (
                        Velocity[i], random.NextDouble(), random.NextDouble(),
                        Position[i], focalParticle.Position[i], PositionPBest[i]
                        );

                }
            }
        }
    }
}
