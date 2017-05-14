using Particle_Swarm_Optimization.Common;
using Swarm_Optimization.FitnessFunction.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Particle_Swarm_Optimization.Firefly_Algorithm
{
    public class Swarm
    {
        public List<FireflyParticle> ParticleList { get; set; }
        public List<double> GlobalBestLog { get; set; }

        public Swarm(EFunction function)
        {
            FireflyParticle.ClearStaticFields();
            ParticleList = FireflyParticle.CreateSwarm(function);
        }

        public void InitializeSwarm()
        {
            foreach(FireflyParticle particle in ParticleList)
            {
                particle.Initialize();
            }
        }

        public void UpdatePopulation(bool saveFitnessLog = false)
        {
            if (saveFitnessLog)
                GlobalBestLog = new List<double>();

            for (int i = 0; i< Parameters.ITERATION_AMOUNT; i++)
            {
                foreach(FireflyParticle pA in ParticleList)
                {
                    //Console.WriteLine("PB: {0}", FireflyParticle.GlobalBest);
                    //if (pA == ParticleList[1]) Console.WriteLine("X: {0} / Y:{1}", FireflyParticle.PositionGBest[0], FireflyParticle.PositionGBest[1]);
                    
                    foreach (FireflyParticle pB in ParticleList)
                    {
                        if (pA == pB) continue;

                        pA.UpdateAttractiveness(pB);
                        pA.UpdatePosition(pB);
                        pA.UpdateFitness();
                    }

                    pA.ForceBoundaries();
                }

                FireflyParticle.UpdateAttractivenessFactor();

                if (saveFitnessLog)
                {
                    GlobalBestLog.Add(FireflyParticle.GlobalBest);
                }
            }
        }
    }
}
