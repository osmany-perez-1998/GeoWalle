using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiling
{
    public class ShapePointsParser : IPrefixParser
    {
        public ShapePointsParser(KeyParser parser)
        {
            Register(parser);
        }
        public Expression Parse(KeyParser parser, Token token, List<CompilingError> errors)
        {

           if(!parser.MatchValue(TokenValues.OpenBracket))
            {
                errors.Add(new CompilingError(token.Location, ErrorCode.Expected, "Open Bracket"));
                return null;
            }
            {
                Expression body = parser.ParseExpression(errors);
                if (body == null)
                {
                    errors.Add(new CompilingError(token.Location, ErrorCode.Invalid, "Expression"));
                    return null;
                }

                if (!parser.MatchValue(TokenValues.ClosedBracket))
                {
                    errors.Add(new CompilingError(token.Location, ErrorCode.Expected, "Closed Bracket"));
                    return null;
                }
                
                return new ShapePointsConstructorNode(token.Location,body);
            }

            
        }

        public void Register(KeyParser parser)
        {
            parser.RegisterPrefixParserByValue("points", this);
        }
    }
}
