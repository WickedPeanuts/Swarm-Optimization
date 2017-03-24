using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Particle_Swarm_Optimization.FitnessFunction
{
    public abstract class AbstractFunction
    {
        public abstract float BOUNDARY_MAX { get; }
        public abstract float BOUNDARY_MIN { get; }

        public abstract double CalculateFitness(double[] position);

        public static double EuclidianDistance(double[] a, double[] b)
        {
            double sum = 0;

            for(int i = 0; i< a.Length; i++)
            {
                sum += Math.Pow(b[i] - a[i], 2);
            }

            return Math.Sqrt(sum);
        }
    }
}
