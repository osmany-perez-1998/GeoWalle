using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiling
{
    public class IfThenElseParser : IPrefixParser
    {
        public IfThenElseParser(KeyParser Parser)
        {
            Register(Parser);
        }
        public Expression Parse(KeyParser parser, Token token, List<CompilingError> errors)
        {
            Expression ifExp = parser.ParseExpression(errors);
            if (ifExp == null)
            {
                errors.Add(new CompilingError(token.Location, ErrorCode.Invalid, "Expression"));
                return null;
            }

            if (!parser.MatchValue(TokenValues.Then))
            {
                errors.Add(new CompilingError(token.Location, ErrorCode.Invalid, "Expression"));
                return null;
            }

            Expression thenExp = parser.ParseExpression(errors);

            if (!parser.MatchValue(TokenValues.Else))
            {
                errors.Add(new CompilingError(token.Location, ErrorCode.Invalid, "Expression"));
                return null;
            }

            Expression elseExp = parser.ParseExpression(errors);

            return new IfThenElseNode(token.Location, ifExp, thenExp, elseExp);
        }

        public void Register(KeyParser parser)
        {
            parser.RegisterPrefixParserByValue(TokenValues.If, this);
        }
    }
}
