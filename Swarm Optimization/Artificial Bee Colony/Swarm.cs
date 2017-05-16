using Particle_Swarm_Optimization.Artificial_Bee_Colony.Enum;
using Particle_Swarm_Optimization.Common;
using Swarm_Optimization.FitnessFunction.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Particle_Swarm_Optimization.Artificial_Bee_Colony
{
    class Swarm
    {
        public List<Bee> ScoutParticleList { get; set; }
        public List<Bee> OnlookerParticleList { get; set; }
        public List<Bee> EmployedParticleList { get; set; }
        public List<double> GlobalBestLog { get; set; }

        public Swarm(EFunction function)
        {
            Bee.ClearStaticFields();

            int employedBees = (Parameters.PARTICLE_AMOUNT * Parameters.EMPLOYED_BEE_AMOUNT / 100);
            employedBees = Math.Min(Parameters.INITIAL_FOOD_SOURCES, employedBees);

            int scoutBees = (Parameters.PARTICLE_AMOUNT * Parameters.SCOUT_BEE_AMOUNT / 100);
            scoutBees = Math.Min(Parameters.INITIAL_FOOD_SOURCES, scoutBees);

            int onlookerBees = Parameters.PARTICLE_AMOUNT - employedBees - scoutBees;

            OnlookerParticleList = Bee.CreateSwarm(function, EBee.Onlooker, onlookerBees);
            ScoutParticleList = Bee.CreateSwarm(function, EBee.Scout, scoutBees);
            EmployedParticleList = Bee.CreateSwarm(function, EBee.Employed, employedBees);

            Bee.CreateRandomFoodSources();
        }

        public void InitializeSwarm()
        {
            ScoutParticleList.ForEach(x => x.Initialize());
            OnlookerParticleList.ForEach(x => x.Initialize());
            EmployedParticleList.ForEach(x => x.Initialize());
        }

        public void UpdatePopulation(bool saveFitnessLog = false)
        {
            if (saveFitnessLog)
                GlobalBestLog = new List<double>();

            for (int i = 0; i < Bee.FoodSourceList.Count; i++) {
                EmployedParticleList[i].DeployFoodSourceForParticle(i);
                ScoutParticleList[i].DeployFoodSourceForParticle(i);
            }
            
            for (int i = 0; i < Parameters.ITERATION_AMOUNT; i++)
            {
                Bee.CalculateFoodSourceFitness();
                Bee.CalculateFoodSourceProbability();
                Bee.SortFoodSourceByProbability();

                EmployedParticleList.ForEach(x =>
                {
                    x.SendEmployedBees();
                    x.UpdateFitness();
                });

                OnlookerParticleList.ForEach(x =>
                {
                    x.SendOnlookerBees();
                    x.UpdateFitness();
                });

                ScoutParticleList.ForEach(x =>
                {
                    x.SendScoutBees();
                });

                if (saveFitnessLog)
                {
                    GlobalBestLog.Add(Bee.GlobalBestFitness);
                }

                Console.WriteLine(i + " " + Bee.GlobalBestFitness);
            }
        }

    }
}
