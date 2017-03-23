using Particle_Swarm_Optimization;
using PSO.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSO
{
    public class Swarm
    {
        public List<AbstractParticle> ParticleList { get; set; }

        public Swarm(ETopology topology, EFunction function, EParameter parameter)
        {
            AbstractParticle.ClearStaticFields();
            ParticleList = AbstractParticle.CreateSwarm(topology, function, parameter, Parameters.PARTICLE_AMMOUNT);
        }

        public void InitializeSwarm()
        {
            foreach (AbstractParticle particle in ParticleList)
            {
                particle.Initialize();
            }
        }

        public void UpdatePopulation()
        {
            for (int i = 0; i < Parameters.ITERATION_AMMOUNT; i++)
            {
                foreach(AbstractParticle particle in ParticleList)
                {
                    particle.UpdateSpeed();
                    particle.UpdatePosition();
                    particle.ForceBoundaries();
                    //if (particle == ParticleList[0])
                    //{
                    //    Console.WriteLine("PX: {0}/PY: {1}/ PZ: {2}/ FIT: {3} / FBest: {4}", particle.Position[0], particle.Position[1], particle.Position[2], particle.Fitness, AbstractParticle.GlobalBest);
                    //    Console.WriteLine("SX: {0}/SY: {1}/ SZ: {2}", particle.Velocity[0], particle.Velocity[1], particle.Velocity[2]);
                    //}
                    particle.UpdateFitness();
                     

                }
                Console.WriteLine("Iteração " + i + ": GBest: " + AbstractParticle.GlobalBest);
            }
        }
    }
}
