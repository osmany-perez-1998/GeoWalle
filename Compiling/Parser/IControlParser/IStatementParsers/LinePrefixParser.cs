using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiling
{
    class LinePrefixParser : IStatementParser
    {
        public LinePrefixParser(KeyParser Parser)
        {
            Register(Parser);
        }
        public Statement Parse(KeyParser parser, Token token,List<CompilingError> errors)
        {
            if (!parser.MatchValue(TokenValues.Line)) return null;          


            if (parser.MatchValue(TokenValues.Sequence) && parser.MatchType(TokenType.Identifier))
            {
                if (!parser.MatchValue(TokenValues.StatementSeparator))
                {
                    errors.Add(new CompilingError(token.Location, ErrorCode.Expected, "Statement separator"));
                    return null;
                }
                return new LineSequenceInput(token.Location,parser.LookFromIndex(-2).Value);
            }

            if (parser.MatchType(TokenType.Identifier))
            {
                if (!parser.MatchValue(TokenValues.StatementSeparator))
                {
                    errors.Add(new CompilingError(token.Location, ErrorCode.Expected, "Statement separator"));
                    return null;
                }
                return new LineInput(token.Location,parser.LookFromIndex(-2).Value);
            }

            return null;
        }

        public void Register(KeyParser parser)
        {
            parser.RegisterIStatementParserByValue(TokenValues.Line, this);
        }
    }
}
