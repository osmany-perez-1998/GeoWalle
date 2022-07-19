using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiling
{
    public class LetInParser : IPrefixParser
    {
        public LetInParser(KeyParser Parser)
        {
            Register(Parser);
        }

        public Expression Parse(KeyParser parser, Token token,List<CompilingError> errors)
        {
            List<Statement> let = new List<Statement>();

            do
            {
                Statement Add = parser.ParseStatement(errors);
                if (Add == null)
                {
                    errors.Add(new CompilingError(token.Location, ErrorCode.Invalid, "Statement"));
                    return null;
                }

                let.Add(Add);

            } while (!parser.MatchValue(TokenValues.In));

            Expression inExp = parser.ParseExpression(errors);

            if (inExp == null)
            {
                errors.Add(new CompilingError(token.Location, ErrorCode.Invalid, "Expression"));
                return null;
            }

            return new LetInNode(token.Location,let, inExp);
        }

        public void Register(KeyParser parser)
        {
            parser.RegisterPrefixParserByValue(TokenValues.Let, this);
        }
    }
}
