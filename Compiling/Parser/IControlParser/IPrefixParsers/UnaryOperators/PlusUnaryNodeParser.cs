using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiling
{
    public class PlusUnaryNodeParser : IPrefixParser
    {
        public PlusUnaryNodeParser(KeyParser parser)
        {
            Register(parser);
        }
        public Expression Parse(KeyParser parser, Token token, List<CompilingError> errors)
        {
            Expression aux = parser.ParseExpression(errors);
            if (aux == null)
            {
                errors.Add(new CompilingError(token.Location, ErrorCode.Invalid, "Expression"));
                return null;
            }
            return new PlusUnaryOperatorNode(token.Location,aux);
        }

        public void Register(KeyParser parser)
        {
            parser.RegisterPrefixParserByValue(TokenValues.Add, this);
        }
    }
}
