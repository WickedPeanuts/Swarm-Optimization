using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Particle_Swarm_Optimization.FitnessFunction
{
    class Rosenbrock : AbstractFunction
    {
        public override float BOUNDARY_MAX { get { return 2; } }

        public override float BOUNDARY_MIN { get { return -2; } }

        public override double CalculateFitness(double[] position)
        {
            double sum = 0;

            for (int x = 0; x < position.Length - 1; x++)
            {
                sum += (100 * Math.Pow((Math.Pow(position[x], 2) - position[x + 1]), 2) + Math.Pow((position[x] - 1), 2));
            }

            return sum;
        }
    }
}
