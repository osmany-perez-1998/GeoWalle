using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiling
{
    public class Discriminante
    {
        private double _a;
        private double _b;
        private double _c;
        private double _discriminante;
        public Discriminante(double a, double b, double c)
        {
            _a = a;
            _b = b;
            _c = c;
        }

        public double CalculateDiscriminante()
        {
            _discriminante = Math.Pow(_b, 2) - 4 * _a * _c;
            return _discriminante;
        }

        public void CalculateValues(out int x1,out int x2)
        {
            x1 = (int)((-_b + Math.Sqrt(_discriminante)) / (2 * _a));
            x2 = (int)((-_b - Math.Sqrt(_discriminante)) / (2 * _a));
        }

        public void CalculateOneValue(out int x)
        {
            x = (int)(-_b  / (2 * _a));
        }

    }
}
