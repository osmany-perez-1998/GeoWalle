using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiling
{
    class FunctionCallParser : IInfixParser
    {

        public FunctionCallParser(KeyParser parser)
        {
            Register(parser);
        }

        public Expression Parse(KeyParser parser, Expression left, Token token, List<CompilingError> errors)
        {

            List<Expression> args = new List<Expression>();
            IdNode id = left as IdNode;
            if (id == null)
            {
                errors.Add(new CompilingError(token.Location, ErrorCode.Invalid, "Function name expected"));
                return null;
            }
            //  if (parser.MatchValue(TokenValues.OpenBracket))
            {
                do
                {
                    Expression toAdd = parser.ParseExpression(errors);
                    if (toAdd == null)
                    {
                        errors.Add(new CompilingError(token.Location, ErrorCode.Invalid, "Expression"));
                        return null;
                    }
                    args.Add(toAdd);
                } while (parser.MatchValue(TokenValues.ValueSeparator));

                if (!parser.MatchValue(TokenValues.ClosedBracket))
                {
                    errors.Add(new CompilingError(token.Location, ErrorCode.Expected, "Closed Bracket"));
                    return null;
                }
                return new FunctionCallNode(token.Location,id.Value, args);
            }

            // return null;
        }

        public int GetPrecedence()
        {
            return Precedence.CALL;
        }

        public void Register(KeyParser parser)
        {
            parser.RegisterInfixParserByValue(TokenValues.OpenBracket, this);
        }
    }
}
