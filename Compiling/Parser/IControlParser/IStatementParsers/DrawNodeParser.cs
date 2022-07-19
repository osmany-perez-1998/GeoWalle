using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiling
{
    public class DrawNodeParser : IStatementParser
    {
        public DrawNodeParser(KeyParser Parser)
        {
            Register(Parser);
        }
        public Statement Parse(KeyParser parser, Token token, List<CompilingError> errors)
        {
            string label = "";
            if (!parser.MatchValue(TokenValues.Draw)) return null;

            Expression toDraw = parser.ParseExpression(errors);

            if (toDraw == null)
            {
                errors.Add(new CompilingError(token.Location, ErrorCode.Invalid, "Expression"));
                return null;
            }
            if (parser.MatchValue(TokenValues.ValueSeparator))
            {
                if (!parser.MatchType(TokenType.Text))
                {
                    errors.Add(new CompilingError(token.Location, ErrorCode.Expected, "A label for the drawing"));
                }
                label = parser.LookFromIndex(-1).Value;
            }

            if (!parser.MatchValue(TokenValues.StatementSeparator))
            {
                errors.Add(new CompilingError(token.Location, ErrorCode.Expected, "Statement separator"));
                return null;
            }

            return new DrawNode(token.Location,toDraw, label);
        }

        public void Register(KeyParser parser)
        {
            parser.RegisterIStatementParserByValue(TokenValues.Draw, this);
        }
    }
}
