using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiling
{
    class PointConstructorParser : IPrefixParser
    {
        public PointConstructorParser(KeyParser parser)
        {
            Register(parser);
        }
        public Expression Parse(KeyParser parser, Token token, List<CompilingError> errors)
        {
            Expression p1;
            Expression p2;
           
            if (!parser.MatchValue(TokenValues.OpenBracket))
            {
                errors.Add(new CompilingError(token.Location, ErrorCode.Expected, "Open Bracket"));
            }
            {
                p1 = parser.ParseExpression(errors);
                if (p1 == null)
                {
                    errors.Add(new CompilingError(token.Location, ErrorCode.Invalid, "Expression"));

                    return null;
                }
                if (!parser.MatchValue(TokenValues.ValueSeparator))
                {
                    errors.Add(new CompilingError(token.Location, ErrorCode.Expected, "Coma"));
                    return null;
                }
                {
                    p2 = parser.ParseExpression(errors);
                    if (p2 == null)
                    {
                        errors.Add(new CompilingError(token.Location, ErrorCode.Invalid, "Expression"));

                        return null;
                    }
                    if (!parser.MatchValue(TokenValues.ClosedBracket))
                    {
                        errors.Add(new CompilingError(token.Location, ErrorCode.Expected, "Closed Bracket"));
                        return null;
                    }
                    return new PointConstructorNode(token.Location,p1, p2);
                    
                }
            }
            
        }

        public void Register(KeyParser parser)
        {
            parser.RegisterPrefixParserByValue(TokenValues.Point, this);
        }
    }
}
