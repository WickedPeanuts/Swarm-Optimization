using Particle_Swarm_Optimization;
using Particle_Swarm_Optimization.FitnessFunction;
using PSO.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSO
{
    public abstract class AbstractParticle
    {
        public double[] Position { get; set; }
        public double[] PositionPBest { get; set; }
        public double PersonalBest { get; set; }

        public static double GlobalBest { get; set; }
        public static double[] PositionGBest { get; set; }

        protected static int BOUNDARY_MAX;
        protected static int BOUNDARY_MIN;

        protected EFunction function;
        protected EParameter parameter;

        protected double w = 0;

        protected Random random = new Random();

        protected static readonly double C1 = 2.05;
        protected static readonly double C2 = 2.05;
        protected static readonly double WF = 0.5 / Parameters.ITERATION_AMMOUNT;
        
        public AbstractParticle(EFunction function, EParameter parameter)
        {
            if (function == EFunction.Sphere)
            {
                BOUNDARY_MAX = BasicFunctions.SPHERE_BOUNDARY_MAX;
                BOUNDARY_MIN = BasicFunctions.SPHERE_BOUNDARY_MIN;
            }
            else if (function == EFunction.Rosenbrock)
            {
                BOUNDARY_MAX = BasicFunctions.ROSENBROCK_BOUNDARY_MAX;
                BOUNDARY_MIN = BasicFunctions.ROSENBROCK_BOUNDARY_MIN;
            }
            else if (function == EFunction.RotatedRastrigin)
            {
                BOUNDARY_MAX = BasicFunctions.ROTATEDRASTRIGIN_BOUNDARY_MAX;
                BOUNDARY_MIN = BasicFunctions.ROTATEDRASTRIGIN_BOUNDARY_MIN;
            }

            if (parameter == EParameter.FixedW)
                w = 0.8;
            else if (parameter == EParameter.FloatingW)
                w = 0.9;

            Position = new double[Parameters.DIMENSION_AMMOUNT];
        }

        public virtual void Initialize()
        {
            for (int i = 0; i < Position.Length; i++)
            {
                Position[i] = random.NextDouble() * random.Next(BOUNDARY_MIN, BOUNDARY_MAX);
            }

            PositionPBest = Position;
            PersonalBest = calculateFitness();

            if (GlobalBest == 0 && PositionGBest == null)
            {
                PositionGBest = PositionPBest;
                GlobalBest = PersonalBest;
            }

            UpdateFitness(PersonalBest);
        }

        public abstract void UpdatePosition();
        
        public static List<AbstractParticle> CreateSwarm(ETopology topology, EFunction function, EParameter parameter, int particleAmmount)
        {
            List<AbstractParticle> swarm = new List<AbstractParticle>();

            switch (topology)
            {
                case (ETopology.Local):
                    break;
                case (ETopology.Global):

                    for (int i = 0; i < particleAmmount; i++)
                    {
                        swarm.Add(new GlobalParticle(function, parameter, swarm));
                    }

                    break;

                case (ETopology.Focal):
                    break;
            }

            return swarm;
        }

        protected virtual double calculateFitness()
        {
            if (function == EFunction.Sphere)
                return BasicFunctions.SphereFunction(Position);
            else if (function == EFunction.RotatedRastrigin)
                return BasicFunctions.RotatedRastrigin(Position);
            else
                return BasicFunctions.Rosenbrock(Position);
        }

        protected abstract double[] GenerateNextPosition();

        protected abstract void UpdateFitness(double newPBest);
    }
}
