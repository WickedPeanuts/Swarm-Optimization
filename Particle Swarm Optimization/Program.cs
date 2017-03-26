using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PSO;
using PSO.Enum;
using Particle_Swarm_Optimization.PSO;
using Particle_Swarm_Optimization.FitnessFunction;

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
            //pso.Add(new Swarm(ETopology.Global, EFunction.RotatedRastrigin, EConstrictionFactor.FloatingInertia)); Done!
            //pso.Add(new Swarm(ETopology.Global, EFunction.RotatedRastrigin, EConstrictionFactor.ClercConstriction)); Done!

            //pso.Add(new Swarm(ETopology.Global, EFunction.Rosenbrock, EConstrictionFactor.FixedInertia)); Done!
            //pso.Add(new Swarm(ETopology.Global, EFunction.Rosenbrock, EConstrictionFactor.FloatingInertia)); Done!
            //pso.Add(new Swarm(ETopology.Global, EFunction.Rosenbrock, EConstrictionFactor.ClercConstriction)); Done!

            //Local
            //pso.Add(new Swarm(ETopology.Local, EFunction.Sphere, EConstrictionFactor.FixedInertia));
            //pso.Add(new Swarm(ETopology.Local, EFunction.Sphere, EConstrictionFactor.FloatingInertia));
            //pso.Add(new Swarm(ETopology.Local, EFunction.Sphere, EConstrictionFactor.ClercConstriction));

            //pso.Add(new Swarm(ETopology.Local, EFunction.RotatedRastrigin, EConstrictionFactor.FixedInertia));
            //pso.Add(new Swarm(ETopology.Local, EFunction.RotatedRastrigin, EConstrictionFactor.FloatingInertia));
            //pso.Add(new Swarm(ETopology.Local, EFunction.RotatedRastrigin, EConstrictionFactor.ClercConstriction));
            
            if (false)
            {
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
            int i = 0;
            //Global
            //BeginSimulation(ETopology.Global, EFunction.Sphere, EConstrictionFactor.FixedInertia, i++);
            //BeginSimulation(ETopology.Global, EFunction.Sphere, EConstrictionFactor.FloatingInertia, i++);
            //BeginSimulation(ETopology.Global, EFunction.Sphere, EConstrictionFactor.ClercConstriction, i++);

            //BeginSimulation(ETopology.Global, EFunction.RotatedRastrigin, EConstrictionFactor.FixedInertia, i++);
            //BeginSimulation(ETopology.Global, EFunction.RotatedRastrigin, EConstrictionFactor.FloatingInertia, i++);
            //BeginSimulation(ETopology.Global, EFunction.RotatedRastrigin, EConstrictionFactor.ClercConstriction, i++);

            //BeginSimulation(ETopology.Global, EFunction.Rosenbrock, EConstrictionFactor.FixedInertia, i++);
            //BeginSimulation(ETopology.Global, EFunction.Rosenbrock, EConstrictionFactor.FloatingInertia, i++);
            //BeginSimulation(ETopology.Global, EFunction.Rosenbrock, EConstrictionFactor.ClercConstriction, i++);

            //Local
            //BeginSimulation(ETopology.Local, EFunction.Sphere, EConstrictionFactor.FixedInertia, i++);
            //BeginSimulation(ETopology.Local, EFunction.Sphere, EConstrictionFactor.FloatingInertia, i++);
            //BeginSimulation(ETopology.Local, EFunction.Sphere, EConstrictionFactor.ClercConstriction, i++);

            //BeginSimulation(ETopology.Local, EFunction.RotatedRastrigin, EConstrictionFactor.FixedInertia, i++);
            //BeginSimulation(ETopology.Local, EFunction.RotatedRastrigin, EConstrictionFactor.FloatingInertia, i++);
            //BeginSimulation(ETopology.Local, EFunction.RotatedRastrigin, EConstrictionFactor.ClercConstriction, i++);

            //BeginSimulation(ETopology.Local, EFunction.Rosenbrock, EConstrictionFactor.FixedInertia, i++);
            //BeginSimulation(ETopology.Local, EFunction.Rosenbrock, EConstrictionFactor.FloatingInertia, i++);
            //BeginSimulation(ETopology.Local, EFunction.Rosenbrock, EConstrictionFactor.ClercConstriction, i++);

            //Focal
            BeginSimulation(ETopology.Focal, EFunction.Sphere, EConstrictionFactor.FixedInertia, i++);
            BeginSimulation(ETopology.Focal, EFunction.Sphere, EConstrictionFactor.FloatingInertia, i++);
            BeginSimulation(ETopology.Focal, EFunction.Sphere, EConstrictionFactor.ClercConstriction, i++);

            BeginSimulation(ETopology.Focal, EFunction.RotatedRastrigin, EConstrictionFactor.FixedInertia, i++);
            BeginSimulation(ETopology.Focal, EFunction.RotatedRastrigin, EConstrictionFactor.FloatingInertia, i++);
            BeginSimulation(ETopology.Focal, EFunction.RotatedRastrigin, EConstrictionFactor.ClercConstriction, i++);

            BeginSimulation(ETopology.Focal, EFunction.Rosenbrock, EConstrictionFactor.FixedInertia, i++);
            BeginSimulation(ETopology.Focal, EFunction.Rosenbrock, EConstrictionFactor.FloatingInertia, i++);
            BeginSimulation(ETopology.Focal, EFunction.Rosenbrock, EConstrictionFactor.ClercConstriction, i++);


            //Console.ReadKey();
        }

        public static void BeginSimulation(ETopology top, EFunction fun, EConstrictionFactor cons, int id)
        {
            List<List<Double>> lld = new List<List<Double>>();
            for (int i = 0; i < Parameters.SAMPLE_COUNT; i++)
            {
                Swarm s = (new Swarm(top, fun, cons));
                s.InitializeSwarm();
                s.UpdatePopulation(true);

                lld.Add(s.GlobalBestLog);

                Console.WriteLine("Iteração " + i + ": GBest: " + AbstractParticle.GlobalBest);
            }


            SaveToFile(top, fun, cons, lld, id);
        }

        public static void SaveToFile(ETopology top, EFunction fun, EConstrictionFactor cons, List<List<Double>> outputSamples, int id)
        {
            List<String> stringList = new List<string>();
            String s = "";
            for (int i = 0; i < Parameters.SAMPLE_COUNT; i++)
            {
                s += (i + 1) + "\t";
            }

            stringList.Add(s + "Fitness\tIterations (Average from 30 samples)");

            for (int i = 0; i < Parameters.ITERATION_AMOUNT; i++)
            {
                String temp = "";
                Double sum = 0d;
                for (int j = 0; j < Parameters.PARTICLE_AMOUNT; j++)
                {
                    sum += outputSamples[j][i];
                    temp += outputSamples[j][i] + "\t";
                }
                stringList.Add(temp + i + "\t" + sum / Parameters.PARTICLE_AMOUNT);
            }

            System.IO.File.WriteAllLines(@"WriteLines" + top.ToString() + "," + fun.ToString() + "," + cons.ToString() + "," + id + ".txt", stringList.ToArray());
        }

        public static void SaveToFile2(List<List<Double>> outputSamples, int id)
        {
            List<String> stringList = new List<string>();

            stringList.Add("Iterations\tFitness (Avergage from 30 samples)");

            for (int i = 0; i < Parameters.ITERATION_AMOUNT; i++)
            {
                Double sum = 0d;
                for (int j = 0; j < Parameters.PARTICLE_AMOUNT; j++)
                {
                    sum += outputSamples[j][i];
                }
                stringList.Add(i + "\t" + (sum / Parameters.PARTICLE_AMOUNT));
            }

            System.IO.File.WriteAllLines(@"WriteLines" + id + ".txt", stringList.ToArray());
        }
    }
}
