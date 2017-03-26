using Particle_Swarm_Optimization;
using PSO.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Particle_Swarm_Optimization.PSO
{
    public class Swarm
    {
        public List<AbstractParticle> ParticleList { get; set; }
        public List<double> GlobalBestLog { get; set; }

        public Swarm(ETopology topology, EFunction function, EConstrictionFactor constrictionFactor)
        {
            AbstractParticle.ClearStaticFields();
            ParticleList = AbstractParticle.CreateSwarm(topology, function, constrictionFactor, Parameters.PARTICLE_AMOUNT);
        }

        public void InitializeSwarm()
        {
            foreach (AbstractParticle particle in ParticleList)
            {
                particle.Initialize();
            }
        }

        public void UpdatePopulation(bool saveFitnessLog = false)
        {
            if (saveFitnessLog)
                GlobalBestLog = new List<double>();

            for (int i = 0; i < Parameters.ITERATION_AMOUNT; i++)
            {
                foreach(AbstractParticle particle in ParticleList)
                {
                    particle.UpdateSpeed();
                    particle.UpdatePosition();
                    particle.ForceBoundaries();
                    //if (particle == ParticleList[0])
                    //{
                    //    Console.WriteLine("PX: {0}/PY: {1}/ PZ: {2}/ FIT: {3} / FBest: {4}", particle.Position[0], particle.Position[1], particle.Position[2], particle.PersonalBest, AbstractParticle.GlobalBest);
                    //    Console.WriteLine("SX: {0}/SY: {1}/ SZ: {2}", particle.Velocity[0], particle.Velocity[1], particle.Velocity[2]);
                    //    Console.WriteLine("{0}", particle.PersonalBest);
                    //}
                    particle.UpdateFitness();
                }

                if (saveFitnessLog)
                {
                    GlobalBestLog.Add(AbstractParticle.GlobalBest);
                }
                
            }
        }

        public void UpdatePopulation(List<String> resultList)
        {

        }
    }
}
