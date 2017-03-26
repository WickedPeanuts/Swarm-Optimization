using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Particle_Swarm_Optimization
{
    public class Parameters
    {
        public static readonly int PARTICLE_AMOUNT = 30;
        public static readonly int DIMENSION_AMOUNT = 30;
        public static readonly int ITERATION_AMOUNT = 10000;
        public static readonly int SAMPLE_COUNT = 30;

        public static readonly int FOCAL_ADJACENT_PARTICLES = 2;
    }
}
