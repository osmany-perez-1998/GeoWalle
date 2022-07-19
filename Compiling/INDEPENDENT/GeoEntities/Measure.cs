using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiling
{
    public class Measure:IGeoEntities
    {
        public double Value { get; private set; }

        static Random r = new Random();

        public Measure(double value)
        {
            Value = Value;
        }
        public Measure(GeoPoint a=null, GeoPoint b=null)
        {
            if (a == null && b == null) { Value = r.Next(30, 300); }
            else Value =(int) Math.Sqrt(Math.Pow(a.X - b.X, 2) + Math.Pow(a.Y - b.Y,2));
        }

        public string Type()
        {
            return "measure";
        }
    }
}
