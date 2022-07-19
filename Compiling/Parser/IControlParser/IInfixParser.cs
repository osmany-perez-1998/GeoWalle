using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiling
{
    public interface IInfixParser:IControlParser
    {
        Expression Parse(KeyParser parser, Expression left, Token token, List<CompilingError> errors);
        int GetPrecedence();
    }
}
