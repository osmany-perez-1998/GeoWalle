using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiling
{
    public class VariableDeclarationParser : IStatementParser
    {
        public VariableDeclarationParser(KeyParser Parser)
        {
            Register(Parser);
        }
        public Statement Parse(KeyParser parser, Token token, List<CompilingError> errors)
        {
            if (!parser.MatchType(TokenType.Identifier)) return null;

            string identifier = parser.LookFromIndex(-1).Value;
            if (!parser.MatchValue(TokenValues.Assign))
            {
                errors.Add(new CompilingError(token.Location, ErrorCode.Expected, "Assigment sign"));
            }
            {
                Expression body = parser.ParseExpression(errors);
                if (body == null)
                {
                    errors.Add(new CompilingError(token.Location, ErrorCode.Invalid, "Expression"));
                    return null;
                }
                if (!parser.MatchValue(TokenValues.StatementSeparator))
                {
                    errors.Add(new CompilingError(token.Location, ErrorCode.Expected, "Statement separator"));
                    return null;
                }
                return new VariableDeclaration(token.Location,identifier, body);
            }

        }

        public void Register(KeyParser parser)
        {
            parser.RegisterIStatementParserByType(TokenType.Identifier, this);
        }
    }
}
