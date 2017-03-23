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
        
        public GlobalParticle(EFunction function, EParameter parameter, List<AbstractParticle> swarm) : base(function, parameter)
        {
            this.functionType = function;
            this.parameterType = parameter;
            this.Swarm = swarm;
        }

        public override void UpdatePosition()
        {
            for (int i = 0; i< Parameters.DIMENSION_AMMOUNT; i++)
            {
                Position[i] += Velocity[i];
            }
        }

        public override void UpdateSpeed()
        {
            for (int i = 0; i < Parameters.DIMENSION_AMMOUNT; i++)
            {
                /*
                 * Velocidade [Eixo] = Position [Eixo] * Velocidade Atual [Eixo] +
                 *                      Const1 * Random(0,1) * (Posição Atual - Posição G Best) +
                 *                      Const2 * Random(0,1) * (Posição Atual - Posição P Best);
                 */
                Velocity[i] = w * Velocity[i] +
                    C1 * random.NextDouble() * (PositionPBest[i] - Position[i]) +
                    C2 * random.NextDouble() * (PositionGBest[i] - Position[i]);

                if (parameterType == EParameter.FloatingW)
                    w -= WF;
            }
        }
    }
}
