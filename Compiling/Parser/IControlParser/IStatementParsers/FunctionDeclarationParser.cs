using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiling
{
    public class FunctionDeclarationParser : IStatementParser
    {

        public FunctionDeclarationParser(KeyParser Parser)
        {
            Register(Parser);
        }
        public Statement Parse(KeyParser parser, Token token, List<CompilingError> errors)
        {
            string identifier;
            Expression body;
            List<string> Vacio = new List<string>();

            if (parser.LookFromIndex().Type != TokenType.Identifier || parser.LookFromIndex(1).Value != TokenValues.OpenBracket)
            {
                return null;
            }

            identifier = parser.LookFromIndex().Value;
            bool waste = parser.MatchType(TokenType.Identifier) && parser.MatchValue(TokenValues.OpenBracket);
            //Ya se que esto va a dar true y me avanza los token hasta después del primer paréntesis.

            if (parser.LookFromIndex().Value == TokenValues.ClosedBracket)
            {
                if (parser.MatchValue(TokenValues.ClosedBracket))
                {
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
                        errors.Add(new CompilingError(token.Location, ErrorCode.Expected, "Statement separator"));
                        return null;
                    }
                    return new FunctionDeclaration(token.Location,identifier, Vacio.ToArray(), body);
                }
            }

            if (parser.LookFromIndex().Type == TokenType.Identifier)
            {
                do
                {
                    if (!parser.MatchType(TokenType.Identifier))
                    {
                        errors.Add(new CompilingError(token.Location, ErrorCode.Expected, "Identifier"));
                        return null;
                    }
                    Vacio.Add(parser.LookFromIndex(-1).Value);

                } while (parser.MatchValue(TokenValues.ValueSeparator));

                if (!parser.MatchValue(TokenValues.ClosedBracket))
                {
                    errors.Add(new CompilingError(token.Location, ErrorCode.Expected, "Closed Bracket"));
                    return null;
                }
                if (!parser.MatchValue(TokenValues.Assign))
                {
                    errors.Add(new CompilingError(token.Location, ErrorCode.Expected, "Assigment sign"));
                }
                {
                    body = parser.ParseExpression(errors);
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
                    return new FunctionDeclaration(token.Location,identifier, Vacio.ToArray(), body);
                }
            }

            return null;


        }
        public void Register(KeyParser parser)
        {
            parser.RegisterIStatementParserByValue("FuncDeclaration", this); //super dudaaaa
        }
    }
}
