using Particle_Swarm_Optimization.Artificial_Bee_Colony.Enum;
using Particle_Swarm_Optimization.Common;
using Swarm_Optimization.Artificial_Bee_Colony;
using Swarm_Optimization.FitnessFunction;
using Swarm_Optimization.FitnessFunction.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Particle_Swarm_Optimization.Artificial_Bee_Colony
{
    public class Bee
    {
        public double[] Position { get; set; }
        public double[] PersonalBestPosition { get; set; }
        public double PersonalBestFitness { get; set; }
        private FoodSource foodSource;

        public static double GlobalBestFitness { get; set; }
        public static double[] GlobalBestPosition { get; set; }

        protected static EFunction functionType;
        protected static AbstractFunction function;

        public static List<FoodSource> FoodSourceList { get; set; }
        public static double FoodSourceFitness { get; set; }
        
        protected EBee beeType;

        static Random random = new Random();

        public static void ClearStaticFields()
        {
            function = null;
            FoodSourceList = null;
            GlobalBestPosition = null;
            GlobalBestFitness = 0;
        }

        public Bee(EFunction functionType, EBee beeType)
        {

            Position = new double[Parameters.DIMENSION_AMOUNT];
            this.beeType = beeType;

            if (function == null)
            {
                FoodSourceList = new List<FoodSource>();
                Bee.functionType = functionType;
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

            PersonalBestPosition = (double[])Position.Clone();

            PersonalBestFitness = function.CalculateFitness(Position);

            if (GlobalBestFitness == 0 && GlobalBestPosition == null)
            {
                GlobalBestPosition = (double[])PersonalBestPosition.Clone();
                GlobalBestFitness = PersonalBestFitness;
            }

            UpdateFitness();
        }

        internal void SendScoutBees()
        {
            if (foodSource.ExploringBees == 0)
                foodSource.AttemptToChangeSource++;

            if (foodSource.AttemptToChangeSource >= Parameters.ATTEMPT_TO_CHANGE_SOURCE)
            {
                FoodSourceList.Remove(foodSource);
                CreateRandomFoodSources();
                foodSource = FoodSourceList.Last();
            }

            foodSource.ExploringBees = 0;
        }

        public void SendEmployedBees()
        {
            if (FoodSourceList.Count == 0) return;
            
            //Employed Bees
            //GreedySelectionMove
            double[] newPosition = new double[Parameters.DIMENSION_AMOUNT];
            for (int i = 0; i < Parameters.DIMENSION_AMOUNT; i++)
            {
                newPosition[i] = foodSource.Position[i] + (random.NextDouble() - 1) * 2
                    * (foodSource.Position[i] - foodSource.Position[random.Next(Parameters.DIMENSION_AMOUNT)]);
            }

            //ForceBoundaries
            for (int i = 0; i < Parameters.DIMENSION_AMOUNT; i++)
            {
                if (newPosition[i] > function.BOUNDARY_MAX)
                    newPosition[i] = function.BOUNDARY_MAX;
                else if (newPosition[i] < function.BOUNDARY_MIN)
                    newPosition[i] = function.BOUNDARY_MIN;
            }

            double newPositionFitness = function.CalculateFitness(newPosition);

            if (newPositionFitness < PersonalBestFitness)
            {
                foodSource.Position = Position = newPosition;
                foodSource.Fitness = newPositionFitness;
            }

        }

        public static void SortFoodSourceByProbability()
        {
            FoodSourceList.OrderBy(x => x.Probability);
        }

        public void SendOnlookerBees()
        {
            if (FoodSourceList.Count == 0) return;

            //Last is bigger
            double fsSelectionChance = random.NextDouble() * FoodSourceList.Last().Probability;
            FoodSource pivot = null;

            foreach (FoodSource fs in FoodSourceList) {
                if (fsSelectionChance < fs.Probability)
                {
                    pivot = fs;
                    break;
                }
            }

            pivot.ExploringBees++;
            
            double[] newPosition = new double[Parameters.DIMENSION_AMOUNT];

            for (int i = 0; i < Parameters.DIMENSION_AMOUNT; i++)
            {
                newPosition[i] = pivot.Position[i] + (random.NextDouble() - 1) * 2 * (pivot.Position[i] - pivot.Position[random.Next(Parameters.DIMENSION_AMOUNT)]);
            }

            //Greedy selection
            double newPositionFitness = function.CalculateFitness(newPosition);

            if (newPositionFitness < PersonalBestFitness)
            {
                pivot.Position = Position = newPosition;
                pivot.Fitness = newPositionFitness;
            }
        }

        public void UpdateFitness()
        {
            double newPBest = function.CalculateFitness(Position);
            if (PersonalBestFitness > newPBest)
            {
                //Update PBest and current Position
                PersonalBestPosition = (double[])Position.Clone();
                PersonalBestFitness = newPBest;

                if (GlobalBestFitness > newPBest)
                {
                    //Console.WriteLine("F: {0} To: {1}", GlobalBest, newPBest);
                    GlobalBestPosition = (double[])Position.Clone();
                    GlobalBestFitness = newPBest;
                }
            }
        }

        public void DeployFoodSourceForParticle(int i)
        {
            foodSource = FoodSourceList[i];
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

        public static List<Bee> CreateSwarm(EFunction functionType, EBee beeType, int numberOfBees)
        {
            List<Bee> swarm = new List<Bee>();

            if (beeType == EBee.Employed)
            {
                for (int i = 0; i < numberOfBees; i++)
                {
                    swarm.Add(new Bee(functionType, EBee.Employed));
                }
            }
            else if (beeType == EBee.Scout)
            {
                for (int i = 0; i < numberOfBees; i++)
                {
                    swarm.Add(new Bee(functionType, EBee.Scout));
                }
            }
            else if (beeType == EBee.Onlooker)
            {
                for (int i = 0; i < numberOfBees; i++)
                {
                    swarm.Add(new Bee(functionType, EBee.Onlooker));
                }
            }

            return swarm;
        }

        public static void CreateRandomFoodSources()
        {
            //Initialize the positions of PARAM food sources of employed bees, randomly
            //using uniform distribution in the range(F.MAX, F.MIN). 

            int minFoodSource = (FoodSourceList == null) ? 0 : FoodSourceList.Count;

            for (int j = minFoodSource; j < Parameters.INITIAL_FOOD_SOURCES; j++)
            {
                double[] foodSource = new double[Parameters.DIMENSION_AMOUNT];
                for (int i = 0; i < Parameters.DIMENSION_AMOUNT; i++)
                {
                    foodSource[i] = (random.NextDouble() - 1) * 2 * function.BOUNDARY_MAX;
                }

                FoodSourceList.Add(new FoodSource(foodSource, function.CalculateFitness(foodSource)));
            }
        }

        public static void CalculateFoodSourceProbability()
        {
            foreach(FoodSource fs in FoodSourceList)
            {
                fs.Probability = fs.Fitness / FoodSourceFitness;
            }
        }

        public static void CalculateFoodSourceFitness()
        {
            FoodSourceFitness = 0;
            foreach (FoodSource fs in FoodSourceList)
            {
                if (fs.Fitness >= 0)
                {
                    FoodSourceFitness += 1 / (1 + fs.Fitness);
                }
                else
                {
                    FoodSourceFitness += 1 + Math.Abs(fs.Fitness);
                }
            }
        }
    }
}
