using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiling
{
    public abstract class UnaryOperatorNode:Expression
    {
        public Expression ToModify;
    }
}
    