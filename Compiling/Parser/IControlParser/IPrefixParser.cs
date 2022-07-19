using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiling
{
    public interface IPrefixParser:IControlParser
    {
        Expression Parse(KeyParser parser, Token token, List<CompilingError> errors);
    }
}
