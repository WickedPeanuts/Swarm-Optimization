using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swarm_Optimization.FitnessFunction
{
    class RotatedRastrigin : AbstractFunction
    {
        private static AbstractFunction instance = new RotatedRastrigin();
        public static AbstractFunction Instance { get { return instance; } }

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
