using Particle_Swarm_Optimization;
using Particle_Swarm_Optimization.ConstrictionFactor;
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
        
        protected EFunction functionType;
        protected EConstrictionFactor constrictionType;

        protected AbstractFunction function;
        protected AbstractConstrictionFactor constriction;

        protected static Random random = new Random();

        public static void ClearStaticFields()
        {
            GlobalBest = 0;
            PositionGBest = null;
        }
        
        public AbstractParticle(EFunction functionType, EConstrictionFactor parameter)
        {
            if (parameter == EConstrictionFactor.FixedInertia)
                constriction = new FixedInertia();
            else if (parameter == EConstrictionFactor.FloatingInertia)
                constriction = new FloatingInertia();
            else
                constriction = new ClercConstriction();

            Position = new double[Parameters.DIMENSION_AMMOUNT];
            Velocity = new double[Parameters.DIMENSION_AMMOUNT];

            if (functionType == EFunction.Sphere)
                function = new SphereFunction();
            else if (functionType == EFunction.RotatedRastrigin)
                function = new RotatedRastrigin();
            else
                function = new Rosenbrock();
            
        }

        public virtual void Initialize()
        {
            double high = function.BOUNDARY_MAX * 0.1;
            double low = function.BOUNDARY_MIN * 0.1;

            for (int i = 0; i < Position.Length; i++)
            {
                Position[i] = (function.BOUNDARY_MAX - function.BOUNDARY_MIN) * random.NextDouble() + function.BOUNDARY_MIN;
                Velocity[i] = (high - low) * random.NextDouble() + low;
            }

            PositionPBest = (double[])Position.Clone();

            PersonalBest = function.CalculateFitness(Position);

            if (GlobalBest == 0 && PositionGBest == null)
            {
                PositionGBest = (double[])PositionPBest.Clone();
                GlobalBest = PersonalBest;
            }

            UpdateFitness();
        }

        public abstract void UpdatePosition();
        
        public static List<AbstractParticle> CreateSwarm(ETopology topology, EFunction function, EConstrictionFactor constrictionFactor, int particleAmmount)
        {
            List<AbstractParticle> swarm = new List<AbstractParticle>();

            switch (topology)
            {
                case (ETopology.Local):
                    break;
                case (ETopology.Global):

                    for (int i = 0; i < particleAmmount; i++)
                    {
                        swarm.Add(new GlobalParticle(function, constrictionFactor, swarm));
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
                if (Position[i] > function.BOUNDARY_MAX)
                    Position[i] = function.BOUNDARY_MAX;
                else if (Position[i] < function.BOUNDARY_MIN)
                    Position[i] = function.BOUNDARY_MIN;
            }
        }

        public abstract void UpdateSpeed();

        public void UpdateFitness()
        {
            double newPBest = function.CalculateFitness(Position);
            if (PersonalBest > newPBest)
            {
                //Update PBest and current Position
                PositionPBest = (double[])Position.Clone();
                PersonalBest = newPBest;

                if (GlobalBest > newPBest)
                {
                    //Console.WriteLine("F: {0} To: {1}", GlobalBest, newPBest);
                    PositionGBest = (double[])Position.Clone();
                    GlobalBest = newPBest;
                }
            }
        }
    }
}
