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

        public const int SPHERE_BOUNDARY_MAX = 50;
        public const int SPHERE_BOUNDARY_MIN = -50;

        public const int ROTATEDRASTRIGIN_BOUNDARY_MAX = 100;
        public const int ROTATEDRASTRIGIN_BOUNDARY_MIN = -100;

        public const int ROSENBROCK_BOUNDARY_MAX = 100;
        public const int ROSENBROCK_BOUNDARY_MIN = -100;

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
