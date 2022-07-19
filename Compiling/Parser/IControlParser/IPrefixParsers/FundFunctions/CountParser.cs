using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiling
{
    public class CountParser : IPrefixParser
    {
        public CountParser(KeyParser Parser)
        {
            Register(Parser);
        }
        public Expression Parse(KeyParser parser, Token token, List<CompilingError> errors)
        {
           

            if (!parser.MatchValue(TokenValues.OpenBracket))
            {
                errors.Add(new CompilingError(token.Location, ErrorCode.Expected, "Open Bracket"));
            }
            {
                Expression geo = parser.ParseExpression(errors);
                if (geo == null)
                {
                    errors.Add(new CompilingError(token.Location, ErrorCode.Invalid, "Expression"));
                    return null;
                }
                if (!parser.MatchValue(TokenValues.ClosedBracket))
                {
                    errors.Add(new CompilingError(token.Location, ErrorCode.Expected, "Closed Bracket"));
                    return null;
                }
                return new CountNode(token.Location,geo);
            }

            
        }

        public void Register(KeyParser parser)
        {
            parser.RegisterPrefixParserByValue(TokenValues.Count, this);
        }
    }
}
