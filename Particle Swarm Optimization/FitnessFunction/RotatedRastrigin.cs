using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swarm_Optimization.FitnessFunction
{
    class RotatedRastrigin : AbstractFunction
    {
        public override float BOUNDARY_MAX { get { return 5; } }
        public override float BOUNDARY_MIN { get { return -5; } }

        public override double CalculateFitness(double[] position)
        {
            double sum = 0;

            foreach (double x in position)
            {
                sum += (x * x - 10 * Math.Cos(2 * Math.PI * x) + 10);
            }

            return sum;
        }
    }
}
