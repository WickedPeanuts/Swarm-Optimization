using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PSO;
using PSO.Enum;

namespace Particle_Swarm_Optimization
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Swarm> pso = new List<Swarm>();

            //Global
            pso.Add(new Swarm(ETopology.Global, EFunction.Sphere, EParameter.FixedW));
            if (false)
            {
                pso.Add(new Swarm(ETopology.Global, EFunction.Sphere, EParameter.FloatingW));
                pso.Add(new Swarm(ETopology.Global, EFunction.Sphere, EParameter.ClercConstriction));

                pso.Add(new Swarm(ETopology.Global, EFunction.RotatedRastrigin, EParameter.FixedW));
                pso.Add(new Swarm(ETopology.Global, EFunction.RotatedRastrigin, EParameter.FloatingW));
                pso.Add(new Swarm(ETopology.Global, EFunction.RotatedRastrigin, EParameter.ClercConstriction));

                pso.Add(new Swarm(ETopology.Global, EFunction.Rosenbrock, EParameter.FixedW));
                pso.Add(new Swarm(ETopology.Global, EFunction.Rosenbrock, EParameter.FloatingW));
                pso.Add(new Swarm(ETopology.Global, EFunction.Rosenbrock, EParameter.ClercConstriction));

                //Focal
                pso.Add(new Swarm(ETopology.Focal, EFunction.Sphere, EParameter.FixedW));
                pso.Add(new Swarm(ETopology.Focal, EFunction.Sphere, EParameter.FloatingW));
                pso.Add(new Swarm(ETopology.Focal, EFunction.Sphere, EParameter.ClercConstriction));

                pso.Add(new Swarm(ETopology.Focal, EFunction.RotatedRastrigin, EParameter.FixedW));
                pso.Add(new Swarm(ETopology.Focal, EFunction.RotatedRastrigin, EParameter.FloatingW));
                pso.Add(new Swarm(ETopology.Focal, EFunction.RotatedRastrigin, EParameter.ClercConstriction));

                pso.Add(new Swarm(ETopology.Focal, EFunction.Rosenbrock, EParameter.FixedW));
                pso.Add(new Swarm(ETopology.Focal, EFunction.Rosenbrock, EParameter.FloatingW));
                pso.Add(new Swarm(ETopology.Focal, EFunction.Rosenbrock, EParameter.ClercConstriction));

                //Local
                pso.Add(new Swarm(ETopology.Local, EFunction.Sphere, EParameter.FixedW));
                pso.Add(new Swarm(ETopology.Local, EFunction.Sphere, EParameter.FloatingW));
                pso.Add(new Swarm(ETopology.Local, EFunction.Sphere, EParameter.ClercConstriction));

                pso.Add(new Swarm(ETopology.Local, EFunction.RotatedRastrigin, EParameter.FixedW));
                pso.Add(new Swarm(ETopology.Local, EFunction.RotatedRastrigin, EParameter.FloatingW));
                pso.Add(new Swarm(ETopology.Local, EFunction.RotatedRastrigin, EParameter.ClercConstriction));

                pso.Add(new Swarm(ETopology.Local, EFunction.Rosenbrock, EParameter.FixedW));
                pso.Add(new Swarm(ETopology.Local, EFunction.Rosenbrock, EParameter.FloatingW));
                pso.Add(new Swarm(ETopology.Local, EFunction.Rosenbrock, EParameter.ClercConstriction));
            }

            for (int i = 0; i < Parameters.SAMPLE_COUNT; i++)
            {
                pso[0] = (new Swarm(ETopology.Global, EFunction.Sphere, EParameter.FixedW));
                pso[0].InitializeSwarm();
                pso[0].UpdatePopulation();
                //Console.WriteLine("Iteração " + i + ": GBest: " + AbstractParticle.GlobalBest);
                Console.ReadKey();
            }
        }
    }
}
