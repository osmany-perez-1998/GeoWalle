using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiling
{
    abstract public class BinaryOperatorNode:Expression
    {
        public Expression Left;
        public Expression Right;
    }
}
