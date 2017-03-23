using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Particle_Swarm_Optimization.FitnessFunction
{
    public class BasicFunctions
    {
        public const int SPHERE_BOUNDARY_MAX = 50;
        public const int SPHERE_BOUNDARY_MIN = -50;

        public const int ROTATEDRASTRIGIN_BOUNDARY_MAX = 100;
        public const int ROTATEDRASTRIGIN_BOUNDARY_MIN = -100;

        public const int ROSENBROCK_BOUNDARY_MAX = 100;
        public const int ROSENBROCK_BOUNDARY_MIN = -100;

        public static double SphereFunction(double[] position)
        {
            double sum = 0;

            foreach(double x in position)
            {
                sum += x * x;
            }

            return sum;
        }

        public static double RotatedRastrigin(double[] position)
        {
            double sum = 0;

            foreach(double x in position)
            {
                sum += (x * x - 10 * Math.Cos(2 * Math.PI * x) + 10);
            }

            return sum;
        }

        public static double Rosenbrock(double[] position)
        {
            double sum = 0;

            for (int x = 0; x < position.Length-1; x++)
            {
                sum += (100 * Math.Pow((Math.Pow(position[x], 2) - position[x + 1]), 2) + Math.Pow((position[x] - 1), 2));
            }

            return sum;
        }

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
