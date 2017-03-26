using Particle_Swarm_Optimization;
using Particle_Swarm_Optimization.ConstrictionFactor;
using Particle_Swarm_Optimization.FitnessFunction;
using Particle_Swarm_Optimization.PSO;
using PSO.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Particle_Swarm_Optimization.PSO
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
        
        public AbstractParticle(EFunction functionType, EConstrictionFactor constrictionType)
        {
            this.functionType = functionType;
            this.constrictionType = constrictionType;

            if (constrictionType == EConstrictionFactor.FixedInertia)
                constriction = new FixedInertia();
            else if (constrictionType == EConstrictionFactor.FloatingInertia)
                constriction = new FloatingInertia();
            else
                constriction = new ClercConstriction();

            Position = new double[Parameters.DIMENSION_AMOUNT];
            Velocity = new double[Parameters.DIMENSION_AMOUNT];

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

        public void UpdatePosition()
        {
            for (int i = 0; i < Parameters.DIMENSION_AMOUNT; i++)
            {
                Position[i] += Velocity[i];
            }

            constriction.UpdateParameter();
        }

        public static List<AbstractParticle> CreateSwarm(ETopology topology, EFunction function, EConstrictionFactor constrictionFactor, int particleAmmount)
        {
            List<AbstractParticle> swarm = new List<AbstractParticle>();

            for (int i = 0; i < particleAmmount; i++)
            {
                if (topology == ETopology.Local)
                {
                    swarm.Add(new LocalParticle(function, constrictionFactor, swarm));
                }
                else if (topology == ETopology.Global)
                {
                    swarm.Add(new GlobalParticle(function, constrictionFactor));
                }
                else if (topology == ETopology.Focal)
                {
                    swarm.Add(new FocalParticle(function, constrictionFactor, swarm, i == 0));
                }
            }
               
            return swarm;
        }

        public void ForceBoundaries()
        {
            for (int i = 0; i < Parameters.DIMENSION_AMOUNT; i++)
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
