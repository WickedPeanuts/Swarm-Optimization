using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swarm_Optimization.FitnessFunction
{
    class SphereFunction : AbstractFunction
    {

        public override float BOUNDARY_MAX { get { return 50; } }
        public override float BOUNDARY_MIN { get { return -50; } }

        public override double CalculateFitness(double[] position)
        {
            double sum = 0;

            foreach (double x in position)
            {
                sum += x * x;
            }

            return sum;

        }
    }
}
