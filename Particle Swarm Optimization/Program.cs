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
            //pso.Add(new Swarm(ETopology.Global, EFunction.Sphere, EConstrictionFactor.FixedInertia)); Done!
            //pso.Add(new Swarm(ETopology.Global, EFunction.Sphere, EConstrictionFactor.FloatingInertia)); Done!
            //pso.Add(new Swarm(ETopology.Global, EFunction.Sphere, EConstrictionFactor.ClercConstriction)); Done!

            //pso.Add(new Swarm(ETopology.Global, EFunction.RotatedRastrigin, EConstrictionFactor.FixedInertia)); Done!
            pso.Add(new Swarm(ETopology.Global, EFunction.RotatedRastrigin, EConstrictionFactor.FloatingInertia));
            //pso.Add(new Swarm(ETopology.Global, EFunction.RotatedRastrigin, EConstrictionFactor.ClercConstriction));

            if (false)
            {


                pso.Add(new Swarm(ETopology.Global, EFunction.Rosenbrock, EConstrictionFactor.FixedInertia));
                pso.Add(new Swarm(ETopology.Global, EFunction.Rosenbrock, EConstrictionFactor.FloatingInertia));
                pso.Add(new Swarm(ETopology.Global, EFunction.Rosenbrock, EConstrictionFactor.ClercConstriction));

                //Focal
                pso.Add(new Swarm(ETopology.Focal, EFunction.Sphere, EConstrictionFactor.FixedInertia));
                pso.Add(new Swarm(ETopology.Focal, EFunction.Sphere, EConstrictionFactor.FloatingInertia));
                pso.Add(new Swarm(ETopology.Focal, EFunction.Sphere, EConstrictionFactor.ClercConstriction));

                pso.Add(new Swarm(ETopology.Focal, EFunction.RotatedRastrigin, EConstrictionFactor.FixedInertia));
                pso.Add(new Swarm(ETopology.Focal, EFunction.RotatedRastrigin, EConstrictionFactor.FloatingInertia));
                pso.Add(new Swarm(ETopology.Focal, EFunction.RotatedRastrigin, EConstrictionFactor.ClercConstriction));

                pso.Add(new Swarm(ETopology.Focal, EFunction.Rosenbrock, EConstrictionFactor.FixedInertia));
                pso.Add(new Swarm(ETopology.Focal, EFunction.Rosenbrock, EConstrictionFactor.FloatingInertia));
                pso.Add(new Swarm(ETopology.Focal, EFunction.Rosenbrock, EConstrictionFactor.ClercConstriction));

                //Local
                pso.Add(new Swarm(ETopology.Local, EFunction.Sphere, EConstrictionFactor.FixedInertia));
                pso.Add(new Swarm(ETopology.Local, EFunction.Sphere, EConstrictionFactor.FloatingInertia));
                pso.Add(new Swarm(ETopology.Local, EFunction.Sphere, EConstrictionFactor.ClercConstriction));

                pso.Add(new Swarm(ETopology.Local, EFunction.RotatedRastrigin, EConstrictionFactor.FixedInertia));
                pso.Add(new Swarm(ETopology.Local, EFunction.RotatedRastrigin, EConstrictionFactor.FloatingInertia));
                pso.Add(new Swarm(ETopology.Local, EFunction.RotatedRastrigin, EConstrictionFactor.ClercConstriction));

                pso.Add(new Swarm(ETopology.Local, EFunction.Rosenbrock, EConstrictionFactor.FixedInertia));
                pso.Add(new Swarm(ETopology.Local, EFunction.Rosenbrock, EConstrictionFactor.FloatingInertia));
                pso.Add(new Swarm(ETopology.Local, EFunction.Rosenbrock, EConstrictionFactor.ClercConstriction));
            }

            
            List<List<Double>> lld = new List<List<Double>>();

            for (int i = 0; i < Parameters.SAMPLE_COUNT; i++)
            {
                pso[0] = (new Swarm(ETopology.Global, EFunction.RotatedRastrigin, EConstrictionFactor.FloatingInertia));
                pso[0].InitializeSwarm();
                pso[0].UpdatePopulation(true);

               lld.Add(pso[0].GlobalBestLog);

               Console.WriteLine("Iteração " + i + ": GBest: " + AbstractParticle.GlobalBest);
            }

            SaveToFile2(lld);

            Console.ReadKey();
        }

        public static void SaveToFile(List<List<Double>> outputSamples)
        {
            List<String> stringList = new List<string>();
            String s = "";
            for (int i = 0; i < Parameters.SAMPLE_COUNT; i++)
            {
                s += (i + 1) + "\t";
            }

            stringList.Add(s + "Fitness\tIterations (Average from 30 samples)");

            for (int i = 0; i < Parameters.ITERATION_AMMOUNT; i++)
            {
                String temp = "";
                Double sum = 0d;
                for (int j = 0; j < Parameters.PARTICLE_AMMOUNT; j++)
                {
                    sum += outputSamples[j][i];
                    temp += outputSamples[j][i] + "\t";
                }
                stringList.Add(temp + i + "\t" + sum / Parameters.PARTICLE_AMMOUNT);
            }

            System.IO.File.WriteAllLines(@"WriteLines" + (1) + ".txt", stringList.ToArray());
        }

        public static void SaveToFile2(List<List<Double>> outputSamples)
        {
            List<String> stringList = new List<string>();

            stringList.Add("Iterations\tFitness (Avergage from 30 samples)");

            for (int i = 0; i < Parameters.ITERATION_AMMOUNT; i++)
            {
                Double sum = 0d;
                for (int j = 0; j < Parameters.PARTICLE_AMMOUNT; j++)
                {
                    sum += outputSamples[j][i];
                }
                stringList.Add(i + "\t" + (sum / Parameters.PARTICLE_AMMOUNT));
            }

            System.IO.File.WriteAllLines(@"WriteLines" + (1) + ".txt", stringList.ToArray());
        }
    }
}
