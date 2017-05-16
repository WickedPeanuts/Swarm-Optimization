using Particle_Swarm_Optimization.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swarm_Optimization.Artificial_Bee_Colony
{
    public class FoodSource
    {
        public double[] Position { get; set; }
        public double Fitness { get; set; }
        public double Probability { get; set; }
        public int ExploringBees { get; set; }
        public int AttemptToChangeSource { get; set; }

        public FoodSource(double[] Position, double Fitness)
        {
            this.Position = Position;
            this.Fitness = Fitness;
        }
    }
}
