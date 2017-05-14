using Particle_Swarm_Optimization.Constriction_Factor;
using Particle_Swarm_Optimization.Enum;
using System;
using System.Collections.Generic;
using Particle_Swarm_Optimization.Common;
using Swarm_Optimization.FitnessFunction.Enum;
using Swarm_Optimization.FitnessFunction;

namespace Particle_Swarm_Optimization.Particle_Swarm_Optimization
{
    public abstract class AbstractPSOParticle
    {
        public double[] Position { get; set; }
        public double[] PositionPBest { get; set; }
        public double[] Velocity { get; set; }
        public double PersonalBest { get; set; }

        public static double GlobalBest { get; set; }
        public static double[] PositionGBest { get; set; }
        
        protected static EFunction functionType;
        protected static EConstrictionFactor constrictionType;

        protected static AbstractFunction function;
        protected static AbstractConstrictionFactor constriction;

        protected static Random random = new Random();

        public static void ClearStaticFields()
        {
            GlobalBest = 0;
            PositionGBest = null;

            function = null;
            constriction = null;
        }
        
        public AbstractPSOParticle(EFunction functionType, EConstrictionFactor constrictionType)
        {
            Position = new double[Parameters.DIMENSION_AMOUNT];
            Velocity = new double[Parameters.DIMENSION_AMOUNT];

            if (function == null || constriction == null)
            {
                AbstractPSOParticle.functionType = functionType;
                AbstractPSOParticle.constrictionType = constrictionType;
                function = AbstractFunction.InstanceFunction(functionType);
                constriction = AbstractConstrictionFactor.InstanceFunction(constrictionType);
            }
        }

        public virtual void Initialize()
        {
            double high = function.BOUNDARY_MAX * 0.1;
            double low = function.BOUNDARY_MIN * 0.1;

            //Random distribution around the search space
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

        public static List<AbstractPSOParticle> CreateSwarm(ETopology topology, EFunction function, EConstrictionFactor constrictionFactor, int particleAmmount)
        {
            List<AbstractPSOParticle> swarm = new List<AbstractPSOParticle>();

            for (int i = 0; i < particleAmmount; i++)
            {
                if (topology == ETopology.Ring)
                {
                    swarm.Add(new LocalParticle(function, constrictionFactor));
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

            if (topology == ETopology.Ring)
            {
                for (int i = 0; i < swarm.Count; i++)
                {
                    ((LocalParticle)swarm[i]).LinkSwarm(swarm, i);
                }
            }

            return swarm;
        }
    }
}
