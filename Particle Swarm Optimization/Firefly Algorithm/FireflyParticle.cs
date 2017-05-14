using Particle_Swarm_Optimization.Common;
using Particle_Swarm_Optimization.Enum;
using Swarm_Optimization.FitnessFunction;
using Swarm_Optimization.FitnessFunction.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Particle_Swarm_Optimization.Firefly_Algorithm
{
    public class FireflyParticle
    {
        //absorção de luz estática (gamma) 0 a 1
        //brilho inicial (beta) 0.4 a 0.1 (decrementando)

        //Absorcao da luz pode ser estatica, o brilho inicial ira decrementar
        // e a intensidade do fitness ela pode ser usado como o fitness normal..
        // algumas versoes poderam o quanto o fitness sera levaado em consideracao

        //Beta - Brilho Inicial
        public static double AttractivenessFactor = 0.4d;
        public static double AttractivenessDecreasingFactor = 0.3d / Parameters.ITERATION_AMOUNT;

        //Gamma - Fator de produção de luciferina / Absorção de luz
        public static readonly double LuciferinProductionCoefficient = 0.1d;
        
        public double Alpha { get; set; }
        public double Attractiveness { get; set; }

        public double[] Position { get; set; }
        public double[] PositionPBest { get; set; }
        public double PersonalBest { get; set; }

        public static double GlobalBest { get; set; }
        public static double[] PositionGBest { get; set; }

        protected static EFunction functionType;
        protected static AbstractFunction function;

        static Random random = new Random();

        public static void ClearStaticFields()
        {
            GlobalBest = 0;
            PositionGBest = null;
            AttractivenessFactor = 0.4d;
        }

        public FireflyParticle(EFunction functionType)
        {
            Alpha = 1.0d;

            Position = new double[Parameters.DIMENSION_AMOUNT];
            
            if (function == null)
            {
                FireflyParticle.functionType = functionType;
                function = AbstractFunction.InstanceFunction(functionType);
            }
        }

        public void Initialize()
        {
            double high = function.BOUNDARY_MAX * 0.1;
            double low = function.BOUNDARY_MIN * 0.1;

            //Random distribution around the search space
            for (int i = 0; i < Position.Length; i++)
            {
                Position[i] = (function.BOUNDARY_MAX - function.BOUNDARY_MIN) * random.NextDouble() + function.BOUNDARY_MIN;
            }

            PositionPBest = (double[])Position.Clone();

            PersonalBest = function.CalculateFitness(Position);

            if (GlobalBest == 0 && PositionGBest == null)
            {
                //PositionGBest = (double[])PositionPBest.Clone();
                GlobalBest = PersonalBest;
            }

            UpdateFitness();
        }
        public void UpdatePosition(FireflyParticle a)
        {
            for (int i = 0; i < Parameters.DIMENSION_AMOUNT; i++)
            {
                //xi,d ← xi,d + β(xj,d − xi,d ) + α(rand() − 0.5)
                if (a.PersonalBest < this.PersonalBest)
                    this.Position[i] = this.PositionPBest[i]
                        + Attractiveness * (a.PositionPBest[i] - this.Position[i])
                        + Alpha * (random.NextDouble() - 0.5);
                else
                    this.Position[i] = this.Position[i]
                        + Alpha * (random.NextDouble() - 0.5);
            }
        }

        public void UpdateAttractiveness(FireflyParticle a)
        {
            if (a.PersonalBest < this.PersonalBest)
            {
                double distance = AbstractFunction.EuclidianDistance(a.Position, this.Position);
                double exponential = Math.Pow(Math.E, -LuciferinProductionCoefficient * distance);
                Attractiveness = AttractivenessFactor * exponential;
            }
        }

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

        public static List<FireflyParticle> CreateSwarm(EFunction functionType)
        {
            List<FireflyParticle> swarm = new List<FireflyParticle>();

            for (int i = 0; i < Parameters.PARTICLE_AMOUNT; i++)
            {
                swarm.Add(new FireflyParticle(functionType));
            }

            return swarm;
        }

        public static void UpdateAttractivenessFactor()
        {
            AttractivenessFactor -= AttractivenessDecreasingFactor;
        }
    }
}
