using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Particle_Swarm_Optimization.ConstrictionFactor
{
    public abstract class AbstractConstrictionFactor
    {
        protected static readonly double C1 = 2.05;
        protected static readonly double C2 = 2.05;

        public abstract double CalculateVelocity(double velocity, double random1, double random2, double position, double positionGlobalBest, double positionPersonalBest);

        protected double CalculateVelocity(double inertia, double velocity, double random1, double random2, double position, double positionGlobalBest, double positionPersonalBest)
        {
            /*
             * Velocidade [Eixo] = Position [Eixo] * Velocidade Atual [Eixo] +
             *                      Const1 * Random(0,1) * (Posição Atual - Posição G Best) +
             *                      Const2 * Random(0,1) * (Posição Atual - Posição P Best);
             */
            return inertia * velocity +
                C1 * random1 * (positionPersonalBest - position) +
                C2 * random2 * (positionGlobalBest - position);
        }

        public virtual void UpdateParameter() { }
    }
}
