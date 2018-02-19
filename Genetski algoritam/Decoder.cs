using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Genetski_algoritam
{
  public class Decoder
  {
    private readonly double _max;
    private readonly double _min;
    private readonly int _variableBitsCount;
    public Decoder(int variablesCount, int variableBitsCount, double min, double max)
    {
      VariablesCount = variablesCount;
      _variableBitsCount = variableBitsCount;
      _max = max;
      _min = min;
      if (max <= min)
      {
        throw new ArgumentException("Wrong borders");
      }
    }

    public double[] Decode(byte[] binaryValue)
    {
      var variable = new double[VariablesCount];
      for (var i = 0; i < VariablesCount; i++)
      {
        // pretvori iz binarnog u dekadski
        variable[i] = 0;
        for (var j = i * _variableBitsCount; j < _variableBitsCount * (i + 1); j++)
        {
          variable[i] += binaryValue[j] * Math.Pow(2, _variableBitsCount * (i + 1) - j - 1);
        }
        // x = xmin + k/(2^n - 1 ) * (xmax - xmin)
        variable[i] = _min + variable[i] / (Math.Pow(2, _variableBitsCount) - 1) * (_max - _min);

      }
      return variable;
    }

    public int VariablesCount { get; }
    public int BitCount => VariablesCount * _variableBitsCount;
  }
}
