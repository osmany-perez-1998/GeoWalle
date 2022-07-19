using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiling
{
    public class Number : IGeoEntities
    {
       
        public double Value { get; private set; }
        public Number(double value=0) { Value = value; }

        public string Type()
        {
            return "number";
        }
    }
}
