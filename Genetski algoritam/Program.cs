using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Genetski_algoritam
{
  public class Program
  {
    static void Main(string[] args)
    {
      // probability of crossing = 0.8, probability of mutation = 0.07, population size = 50
      // number of iterations = 1000, number of variables / dimensionality = 1, number of bits by variables = 100
      // minimum variable = -5, maximum variable = 5
      var genetski = new GeneticAlgorithm(0.9, 0.07, 50, 1000, 1, 100, -5, 5);
      Console.WriteLine("Algorithm end, press 'Enter' for exit...");
      Console.Read();

    }
  }
}
