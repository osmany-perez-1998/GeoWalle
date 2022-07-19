using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiling
{
    public class RamdomsParser : IPrefixParser
    {
        public RamdomsParser(KeyParser parser)
        {
            Register(parser);
        }
        public Expression Parse(KeyParser parser, Token token, List<CompilingError> errors)
        {
            

            if (!parser.MatchValue(TokenValues.OpenBracket) || !parser.MatchValue(TokenValues.ClosedBracket))
            {
                errors.Add(new CompilingError(token.Location, ErrorCode.Expected, "Bracket"));
                return null;
            }
            {
                return new RandomsConstructorNode(token.Location);
            }

            
        }

        public void Register(KeyParser parser)
        {
            parser.RegisterPrefixParserByValue("randoms", this);
        }
    }
}

