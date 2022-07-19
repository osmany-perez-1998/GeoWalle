using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiling
{
    public static class Precedence
    {
        public static int OR = 0;
        public static int AND = 10;
        public static int ASSIGMENT = 50;
        public static int CONDITIONAL = 100;
        public static int SUM_SUBSTRACT = 150;
        public static int PRODUCT_DIVISION = 200;
        public static int EXPONENT_ROOT = 250;
        public static int PREFIX = 300;
        public static int POSTFIX = 350;
        public static int CALL = 400;
    }
}
