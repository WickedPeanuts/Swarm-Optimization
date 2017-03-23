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
            this.function = function;
            this.parameter = parameter;
            this.Swarm = swarm;
        }

        public override void UpdatePosition()
        {
            Position = GenerateNextPosition();

            UpdateFitness(
                    calculateFitness()
                );
        }

        protected override void UpdateFitness(double newPBest)
        {
            if (PersonalBest > newPBest)
            {
                //Update PBest and current Position
                PositionPBest = Position;
                PersonalBest = newPBest;

                if (GlobalBest > newPBest)
                {
                    PositionGBest = Position;
                    GlobalBest = newPBest;
                }
            }
        }

        protected override double[] GenerateNextPosition()
        {
            double[] nextPosition = new double[Position.Length];

            for (int i = 0; i < nextPosition.Length; i++)
            {
                /*
                 * Próx Posição[Eixo] = Velocidade * Posição Atual [Eixo] +
                 *                      Const1 * Random(0,1) * (Posição Atual - Posição G Best) +
                 *                      Const2 * Random(0,1) * (Posição Atual - Posição P Best);
                 */
                nextPosition[i] = w * Position[i] +
                    C1 * random.NextDouble() * (Position[i] - PositionPBest[i]) +
                    C2 * random.NextDouble() * (Position[i] - PositionGBest[i]);

                if (parameter == EParameter.FloatingW)
                    w -= WF;

            }

            return nextPosition;
        }
    }
}
