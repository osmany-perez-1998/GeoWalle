using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiling
{
   public static class Operations
    {
        public static Measure Add(Measure m1,Measure m2)
        {
            return new Measure(m1.Value + m2.Value);
        }
        public static Number Add(Number n1, Number n2)
        {
            return new Number(n1.Value + n2.Value);
        }

        public static Measure Substract(Measure m1, Measure m2)
        {
            return new Measure(m1.Value - m2.Value);
        }
        public static Number Substract(Number n1, Number n2)
        {
            return new Number(n1.Value - n2.Value);
        }

        public static Number Division(Measure m1,Measure m2)
        {
            return new Number((int)(m1.Value / m2.Value));
        }
        public static Number Division(Number n1, Number n2)
        {
            return new Number(n1.Value / n2.Value);
        }

        public static Measure Product(Measure m1, Number n1)
        {
            return new Measure(m1.Value * n1.Value);
        }
        public static Number Product(Number n1, Number n2)
        {
            return new Number(n1.Value * n2.Value);
        }
             
    }
}
