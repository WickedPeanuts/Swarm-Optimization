using Particle_Swarm_Optimization.Common;
using Particle_Swarm_Optimization.Enum;
using Particle_Swarm_Optimization.Firefly_Algorithm;
using Particle_Swarm_Optimization.Particle_Swarm_Optimization;
using Swarm_Optimization.FitnessFunction.Enum;
using System;
using System.Collections.Generic;

namespace Particle_Swarm_Optimization
{
    class Program
    {
        static void Main(string[] args)
        {
            int i = 0;

            #region PSO
            //Global
            //BeginSimulationPSO(ETopology.Global, EFunction.Sphere, EConstrictionFactor.FixedInertia, i++);
            //BeginSimulationPSO(ETopology.Global, EFunction.Sphere, EConstrictionFactor.FloatingInertia, i++);
            //BeginSimulationPSO(ETopology.Global, EFunction.Sphere, EConstrictionFactor.ClercConstriction, i++);

            //BeginSimulationPSO(ETopology.Global, EFunction.RotatedRastrigin, EConstrictionFactor.FixedInertia, i++);
            //BeginSimulationPSO(ETopology.Global, EFunction.RotatedRastrigin, EConstrictionFactor.FloatingInertia, i++);
            //BeginSimulationPSO(ETopology.Global, EFunction.RotatedRastrigin, EConstrictionFactor.ClercConstriction, i++);

            //BeginSimulationPSO(ETopology.Global, EFunction.Rosenbrock, EConstrictionFactor.FixedInertia, i++);
            //BeginSimulationPSO(ETopology.Global, EFunction.Rosenbrock, EConstrictionFactor.FloatingInertia, i++);
            //BeginSimulationPSO(ETopology.Global, EFunction.Rosenbrock, EConstrictionFactor.ClercConstriction, i++);

            //Local
            //BeginSimulationPSO(ETopology.Local, EFunction.Sphere, EConstrictionFactor.FixedInertia, 1);
            //BeginSimulationPSO(ETopology.Local, EFunction.Sphere, EConstrictionFactor.FloatingInertia, i++);
            //BeginSimulationPSO(ETopology.Local, EFunction.Sphere, EConstrictionFactor.ClercConstriction, i++);

            //BeginSimulationPSO(ETopology.Local, EFunction.RotatedRastrigin, EConstrictionFactor.FixedInertia, i++);
            //BeginSimulationPSO(ETopology.Local, EFunction.RotatedRastrigin, EConstrictionFactor.FloatingInertia, i++);
            //BeginSimulationPSO(ETopology.Local, EFunction.RotatedRastrigin, EConstrictionFactor.ClercConstriction, i++);

            //BeginSimulationPSO(ETopology.Local, EFunction.Rosenbrock, EConstrictionFactor.FixedInertia, i++);
            //BeginSimulationPSO(ETopology.Local, EFunction.Rosenbrock, EConstrictionFactor.FloatingInertia, i++);
            //BeginSimulationPSO(ETopology.Local, EFunction.Rosenbrock, EConstrictionFactor.ClercConstriction, i++);

            //Focal
            //BeginSimulationPSO(ETopology.Focal, EFunction.Sphere, EConstrictionFactor.FixedInertia, i++);
            //BeginSimulationPSO(ETopology.Focal, EFunction.Sphere, EConstrictionFactor.FloatingInertia, i++);
            //BeginSimulationPSO(ETopology.Focal, EFunction.Sphere, EConstrictionFactor.ClercConstriction, i++);

            //BeginSimulationPSO(ETopology.Focal, EFunction.RotatedRastrigin, EConstrictionFactor.FixedInertia, i++);
            //BeginSimulationPSO(ETopology.Focal, EFunction.RotatedRastrigin, EConstrictionFactor.FloatingInertia, i++);
            //BeginSimulationPSO(ETopology.Focal, EFunction.RotatedRastrigin, EConstrictionFactor.ClercConstriction, i++);

            //BeginSimulationPSO(ETopology.Focal, EFunction.Rosenbrock, EConstrictionFactor.FixedInertia, i++);
            //BeginSimulationPSO(ETopology.Focal, EFunction.Rosenbrock, EConstrictionFactor.FloatingInertia, i++);
            //BeginSimulationPSO(ETopology.Focal, EFunction.Rosenbrock, EConstrictionFactor.ClercConstriction, i++);
            #endregion PSO

            #region FFA

            BeginSimulationFFA(EFunction.Sphere, i++);

            #endregion FFA

            Console.ReadKey();
        }

        public static void BeginSimulationFFA(EFunction fun, int id)
        {
            List<List<Double>> lld = new List<List<Double>>();
            for (int i = 0; i < Parameters.SAMPLE_COUNT; i++)
            {
                Firefly_Algorithm.Swarm s = (new Firefly_Algorithm.Swarm(fun));
                s.InitializeSwarm();

                s.UpdatePopulation(true);

                lld.Add(s.GlobalBestLog);

                Console.WriteLine("Iteração " + i + ": GBest: " + FireflyParticle.GlobalBest);
            }
            SaveToFile2(ETopology.Global, fun, EConstrictionFactor.FixedInertia, lld, id);
        }

        public static void BeginSimulationPSO(ETopology top, EFunction fun, EConstrictionFactor cons, int id)
        {
            List<List<Double>> lld = new List<List<Double>>();
            for (int i = 0; i < Parameters.SAMPLE_COUNT; i++)
            {
                Particle_Swarm_Optimization.Swarm s = (new Particle_Swarm_Optimization.Swarm(top, fun, cons));
                s.InitializeSwarm();
                s.UpdatePopulation(true);

                lld.Add(s.GlobalBestLog);

                Console.WriteLine("Iteração " + i + ": GBest: " + AbstractPSOParticle.GlobalBest);
            }


            SaveToFile2(top, fun, cons, lld, id);
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

        public static void SaveToFile2(ETopology top, EFunction fun, EConstrictionFactor cons, List<List<Double>> outputSamples, int id)
        {
            List<String> stringList = new List<string>();

            stringList.Add("Iterations\tFitness (Avergage from 30 samples)");
            stringList.Add("{");
            for (int i = 0; i < Parameters.SAMPLE_COUNT; i++)
            {

                stringList.Add((outputSamples[i][outputSamples[i].Count - 1]).ToString().Replace(',', '.') + ",");
            }
            stringList.Add("}");

            System.IO.File.WriteAllLines(@"" + top + ", " + fun + ", " + cons + ", " + id + ".txt", stringList.ToArray());
        }
    }
}
