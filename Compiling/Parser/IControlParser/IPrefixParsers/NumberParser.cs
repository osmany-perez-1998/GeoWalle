using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiling
{
    public class NumberParser : IPrefixParser
    {
        public NumberParser(KeyParser Parser)
        {
            Register(Parser);
        }
        public Expression Parse(KeyParser parser, Token token, List<CompilingError> errors)
        {
            return new ConstantNode(token.Location,double.Parse(token.Value));
        }

        public void Register(KeyParser parser)
        {
            parser.RegisterPrefixParserByType(TokenType.Number, this);
        }
    }
}
