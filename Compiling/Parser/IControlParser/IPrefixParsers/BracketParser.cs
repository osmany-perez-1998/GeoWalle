using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiling
{
    public class BracketParser : IPrefixParser
    {
        public BracketParser(KeyParser Parser)
        {
            Register(Parser);
        }
        public Expression Parse(KeyParser parser, Token token, List<CompilingError> errors)
        {
            Expression expression = parser.ParseExpression(errors);
            if (expression == null)
            {
                errors.Add(new CompilingError(token.Location, ErrorCode.Invalid, "Expression"));
                return null;
            }

            if (!parser.MatchValue(TokenValues.ClosedBracket))
            {
                errors.Add(new CompilingError(token.Location, ErrorCode.Expected, "Closed Bracket"));
            }
            return expression;
        }

   
        public void Register(KeyParser parser)
        {
            parser.RegisterPrefixParserByValue(TokenValues.OpenBracket,this);
        }
    }
}
