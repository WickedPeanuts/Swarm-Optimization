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
                    particle.UpdatePosition();
                }
            }
        }
    }
}
