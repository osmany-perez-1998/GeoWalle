
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiling
{
    public class SequenceParser : IPrefixParser
    {
        public SequenceParser(KeyParser parser)
        {
            Register(parser);
        }
        public Expression Parse(KeyParser parser, Token token, List<CompilingError> errors)
        {
            // if (!parser.MatchValue(TokenValues.OpenCurlyBraces)) return null;
            List<Expression> toReturn = new List<Expression>();

            if (parser.MatchValue(TokenValues.ClosedCurlyBraces)) return new FiniteSequenceNode(token.Location, new List<Expression>());

            Expression nm1 = parser.ParseExpression(errors);
            if (nm1 != null) toReturn.Add(nm1);
            else
            {
                errors.Add(new CompilingError(token.Location, ErrorCode.Invalid, "Expression"));
                return null;
            }
            {
                if (parser.MatchValue("."))
                {
                    errors.Add(new CompilingError(token.Location, ErrorCode.Expected, "Three dots"));
                    return null;
                }
                if (parser.MatchValue(TokenValues.ThreeDots))
                {
                    if (parser.MatchValue(TokenValues.ClosedCurlyBraces))
                        return new InfiniteSequenceNode(token.Location, toReturn[0]);

                    Expression nm2 = parser.ParseExpression(errors);
                    if (nm2 != null) toReturn.Add(nm2);
                    else return null;

                    return new IntervalSequenceNode(token.Location, toReturn[0], toReturn[1]);
                }
            }

            if (parser.MatchValue(TokenValues.ValueSeparator))
            {
                do
                {
                    Expression geo = parser.ParseExpression(errors);
                    if (geo != null) toReturn.Add(geo);
                    else return null;
                } while (parser.MatchValue(TokenValues.ValueSeparator));
            }
            if (parser.MatchValue(TokenValues.ClosedCurlyBraces))
                return new FiniteSequenceNode(token.Location, toReturn);
            return null;


        }

        public void Register(KeyParser parser)
        {
            parser.RegisterPrefixParserByValue(TokenValues.OpenCurlyBraces, this);
        }
    }
}
