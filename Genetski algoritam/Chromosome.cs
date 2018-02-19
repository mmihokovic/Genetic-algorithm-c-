using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Genetski_algoritam
{
  public class Chromosome : IComparable
  {
    private double[] _variable;
    private readonly Decoder _decoder;

    public Chromosome(Decoder decoder)
    {
      _decoder = decoder;
      Bits = new byte[decoder.BitCount];
      Fitness = 0;
      _variable = new double[decoder.VariablesCount];
    }

    public Chromosome(Decoder decoder, Random rand)
    {
      _decoder = decoder;
      Bits = new byte[decoder.BitCount];
      for (var i = 0; i < decoder.BitCount; i++)
      {
        Bits[i] = (byte)rand.Next(0, 2);
      }
      Fitness = 0;
      _variable = new double[decoder.VariablesCount];
    }

    public int CompareTo(object obj)
    {
      if (obj is Chromosome nekiKromosom)
      {
        return Fitness.CompareTo(nekiKromosom.Fitness);
      }
      throw new ArithmeticException("Objekt not chromosome");
    }

    public double Fitness { get; set; }

    public void IzracunajFitness(DelegateFunkcija funkcija)
    {
      Fitness = funkcija(_decoder.Decode(Bits));
    }

    public byte[] Bits { get; set; }
  }
}
