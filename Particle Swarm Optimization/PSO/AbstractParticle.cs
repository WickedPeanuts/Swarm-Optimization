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
        public double[] Velocity { get; set; }
        public double PersonalBest { get; set; }

        public static double GlobalBest { get; set; }
        public static double[] PositionGBest { get; set; }

        protected static int BOUNDARY_MAX;
        protected static int BOUNDARY_MIN;

        protected EFunction function;
        protected EParameter parameter;

        protected double w = 0;

        protected static Random random = new Random();

        protected static readonly double C1 = 2.05;
        protected static readonly double C2 = 2.05;
        protected static readonly double WF = 0.5 / Parameters.ITERATION_AMMOUNT;

        public static void ClearStaticFields()
        {
            GlobalBest = 0;
            PositionGBest = null;
        }
        
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
            Velocity = new double[Parameters.DIMENSION_AMMOUNT];
        }

        public virtual void Initialize()
        {
            double high = BOUNDARY_MAX * 0.1;
            double low = BOUNDARY_MIN * 0.1;

            for (int i = 0; i < Position.Length; i++)
            {
                Position[i] = random.NextDouble() * random.Next(BOUNDARY_MIN, BOUNDARY_MAX);
                Velocity[i] = (high - low) * random.NextDouble() + low;
            }

            PositionPBest = Position;

            PersonalBest = CalculateFitness();

            if (GlobalBest == 0 && PositionGBest == null)
            {
                PositionGBest = PositionPBest;
                GlobalBest = PersonalBest;
            }

            UpdateFitness();
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

        public void ForceBoundaries()
        {
            for (int i = 0; i < Parameters.DIMENSION_AMMOUNT; i++)
            {
                if (function == EFunction.Sphere)
                {
                    if (Position[i] > BasicFunctions.SPHERE_BOUNDARY_MAX)
                        Position[i] = BasicFunctions.SPHERE_BOUNDARY_MAX;
                    else if (Position[i] < BasicFunctions.SPHERE_BOUNDARY_MIN)
                        Position[i] = BasicFunctions.SPHERE_BOUNDARY_MIN;
                }
                else if (function == EFunction.RotatedRastrigin)
                {
                    if (Position[i] > BasicFunctions.ROTATEDRASTRIGIN_BOUNDARY_MAX)
                        Position[i] = BasicFunctions.ROTATEDRASTRIGIN_BOUNDARY_MAX;
                    else if (Position[i] < BasicFunctions.ROTATEDRASTRIGIN_BOUNDARY_MIN)
                        Position[i] = BasicFunctions.ROTATEDRASTRIGIN_BOUNDARY_MIN;
                }
                else if (function == EFunction.Rosenbrock)
                {
                    if (Position[i] > BasicFunctions.ROSENBROCK_BOUNDARY_MAX)
                        Position[i] = BasicFunctions.ROSENBROCK_BOUNDARY_MAX;
                    else if (Position[i] < BasicFunctions.ROSENBROCK_BOUNDARY_MIN)
                        Position[i] = BasicFunctions.ROSENBROCK_BOUNDARY_MIN;
                }
            }
        }

        protected virtual double CalculateFitness()
        {
            if (function == EFunction.Sphere)
                return BasicFunctions.SphereFunction(Position);
            else if (function == EFunction.RotatedRastrigin)
                return BasicFunctions.RotatedRastrigin(Position);
            else
                return BasicFunctions.Rosenbrock(Position);
        }

        public abstract void UpdateSpeed();

        public void UpdateFitness()
        {
            double newPBest = CalculateFitness();
            if (PersonalBest > newPBest)
            {
                //Update PBest and current Position
                PositionPBest = Position;
                PersonalBest = newPBest;

                if (GlobalBest > newPBest)
                {
                    Console.WriteLine("F: {0} To: {1}", GlobalBest, newPBest);
                    PositionGBest = Position;
                    GlobalBest = newPBest;
                }
            }
        }
    }
}
