using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiling
{
    public class VariableCallParser : IPrefixParser
    {
        public VariableCallParser(KeyParser Parser)
        {
            Register(Parser);
        }
        public Expression Parse(KeyParser parser, Token token, List<CompilingError> errors)
        {
            return new IdNode(token.Location,token.Value);
        }

        public void Register(KeyParser parser)
        {
            parser.RegisterPrefixParserByType(TokenType.Identifier, this);
        }
    }
}
