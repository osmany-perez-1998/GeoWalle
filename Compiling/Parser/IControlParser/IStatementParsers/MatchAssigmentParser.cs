using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiling
{
    public class MatchAssigmentParser : IStatementParser
    {
        public MatchAssigmentParser(KeyParser parser)
        {
            Register(parser);
        }
        public Statement Parse(KeyParser parser, Token token, List<CompilingError> errors)
        {
            List<string> varNames = new List<string>();
            Expression body;
            if (parser.LookFromIndex().Type != TokenType.Identifier || parser.LookFromIndex(1).Value != TokenValues.ValueSeparator)
                return null;

            do
            {
                if (!parser.MatchType(TokenType.Identifier) && !parser.MatchValue(TokenValues._))
                {
                    errors.Add(new CompilingError(token.Location, ErrorCode.Expected, "Identifier"));
                    return null;
                }
                varNames.Add(parser.LookFromIndex(-1).Value);

            } while (parser.MatchValue(TokenValues.ValueSeparator));

            if (!parser.MatchValue(TokenValues.Assign))
            {
                errors.Add(new CompilingError(token.Location, ErrorCode.Expected, "Assigment sign"));
                return null;
            }
            body = parser.ParseExpression(errors);

            if (body == null)
            {
                errors.Add(new CompilingError(token.Location, ErrorCode.Invalid, "Expression"));
                return null;
            }
            if (!parser.MatchValue(TokenValues.StatementSeparator))
            {
                errors.Add(new CompilingError(token.Location, ErrorCode.Expected, "Statement Separator"));
                return null;
            }

            return new MatchAssigmentOfSequence(token.Location,varNames.ToArray(), body);
        }

        public void Register(KeyParser parser)
        {
            parser.RegisterIStatementParserByValue("MatchAssigment", this);
        }
    }
}
