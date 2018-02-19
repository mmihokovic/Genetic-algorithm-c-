using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Genetski_algoritam
{
    public delegate double DelegateFunkcija(double[] varijabla);

    public class GeneticAlgorithm
    {
        private const double LinearFitnessStartVal = 100;
        private const double LinearFitnessShift = 5;
        private double _crossoverProbability;
        private double _mutationProbability;
        private readonly int _populationSize;
        private int _iterations;
        private readonly Chromosome[] _population;
        private readonly Decoder _decoder;


        public GeneticAlgorithm(double crossoverProbability, double mutationProbability, int populationSize, int iterations, 
            int brojVar, int brBitova, double varMin, double varMax)
        {
            _crossoverProbability = crossoverProbability;
             _mutationProbability = mutationProbability;
            _populationSize = populationSize;
            _iterations = iterations;
            var rand = new Random();

            // broj varijabli = 1; broj bitova = 10; min = -5; max = 5
            _decoder = new Decoder(brojVar, brBitova, varMin, varMax);

            _population = new Chromosome[populationSize];
            CreatePopulation(populationSize, _decoder, rand ,true);
            EvaluatePopulation(_population);

            for (var i = 0; i < iterations; i++)
            {
                var newPopulation = new Chromosome[populationSize];
                Array.Sort(_population);
                
                //elitizam
                newPopulation[0] = new Chromosome(_decoder);
                newPopulation[1] = new Chromosome(_decoder);

                for (var rbBita = 0; rbBita < _decoder.BitCount; rbBita++)
                {
                    newPopulation[0].Bits[rbBita] = _population[0].Bits[rbBita];
                    newPopulation[1].Bits[rbBita] = _population[1].Bits[rbBita];
                }
                    for (var j = 1; j < populationSize / 2; j++)
                    {
                        var roditelj1 = SelectParent(_population, rand);
                        var roditelj2 = SelectParent(_population, rand);
                        var child1 = new Chromosome(_decoder);
                        var child2 = new Chromosome(_decoder);
                        Crossover(roditelj1, roditelj2, child1, child2, crossoverProbability, rand);
                        Mutate(child1, mutationProbability, rand);
                        Mutate(child2, mutationProbability, rand);
                        newPopulation[2 * j] = child1;
                        newPopulation[2 * j + 1] = child2;
                    }

                EvaluatePopulation(newPopulation);
                _population = newPopulation;
                var best = _population[0];
                for (var k = 0; k < populationSize; k++)
                {
                    if (best.Fitness > _population[k].Fitness)
                    {
                        best = _population[k];
                    }
                }

                Console.Write("current f soultion is: f(");
                
                var sequence = "";
                var decodes = _decoder.Decode(_population[0].Bits);
                for(var pozicija = 0 ; pozicija < _decoder.VariablesCount; pozicija++)
                {
                    if (pozicija > 0)
                        sequence += ", " + decodes[pozicija];
                    else
                        sequence += decodes[pozicija].ToString();
                }

                Console.Write(sequence + ") = " + FunctionForOptimization(_decoder.Decode(_population[0].Bits)) + "\n");
           }
        }

        private double FunctionForOptimization(double[] variable)
        {
            var n = variable.Length;
            double value = 10 * n;
            for (var i = 0; i < n; i++)
            {
                value += variable[i] * variable[i] - 10 * Math.Cos(2 * Math.PI * variable[i]);
            }
            return value;
        }

        private void CreatePopulation(int populationSize, Decoder decoder,Random rand ,bool initiPop)
        {
            if (initiPop)
            {
                for (var i = 0; i < populationSize; i++)
                {
                    _population[i] = new Chromosome(decoder, rand);
                }
            }
            else
            {
                for (var i = 0; i < populationSize; i++)
                {
                    _population[i] = new Chromosome(decoder);
                }
            }
        }

        private void EvaluatePopulation(Chromosome[] population)
        {
            for (var i = 0; i < _populationSize; i++)
            {
                population[i].IzracunajFitness(FunctionForOptimization);
            }

            //podesi fitness
            Array.Sort(population);
            var currentFitness = LinearFitnessStartVal;
            foreach (var kromosom in population)
            {
                kromosom.Fitness = currentFitness;
                if (currentFitness - LinearFitnessShift > 0)
                {
                    currentFitness -= LinearFitnessShift;
                }
            }
        }

        private Chromosome SelectParent(Chromosome[] population, Random rand)
        {
            double fitnessSum = 0;
            foreach (var kromosom in population)
            {
                fitnessSum += kromosom.Fitness;
            }

            var randDouble = rand.NextDouble();
            double selectionProbability = 0;
            foreach (var kromosom in population)
            {
                var len = kromosom.Fitness / fitnessSum;
                if (selectionProbability <= randDouble && randDouble <= (selectionProbability + len))
                {
                    return kromosom;
                }
                else
                {
                    selectionProbability += len;
                }
            }

            throw new ArgumentOutOfRangeException("Parent not selected");
        }

        private void Crossover(Chromosome parent1, Chromosome parent2, Chromosome child1, Chromosome child2,
            double crossoverProbability, Random rand)
        {
            var nextDouble = rand.NextDouble();
            if (nextDouble > crossoverProbability)
            {
                for (var i = 0; i < _decoder.BitCount; i++)
                {
                    // copy bits
                    child1.Bits[i] = parent1.Bits[i];
                    child2.Bits[i] = parent2.Bits[i];
                }
            }
            else
            {
                var breakingPoint = rand.Next(1, _decoder.BitCount - 1);
                for (var i = 0; i < breakingPoint; i++)
                {
                    child1.Bits[i] = parent1.Bits[i];
                    child2.Bits[i] = parent2.Bits[i];
                }
                for (var i = breakingPoint; i < _decoder.BitCount; i++)
                {
                    child1.Bits[i] = parent2.Bits[i];
                    child2.Bits[i] = parent1.Bits[i];
                }
            }
        }

        private void Mutate (Chromosome chromosome, double mutationProbability ,Random rand){
            for (var i = 0; i < _decoder.BitCount; i++)
            {
                var nextDouble = rand.NextDouble();
                if (nextDouble <= mutationProbability)
                {
                    // okreni bit XOR-om
                    chromosome.Bits[i] = (byte)(chromosome.Bits[i] ^1);
                }
            }
        }


    }
}
