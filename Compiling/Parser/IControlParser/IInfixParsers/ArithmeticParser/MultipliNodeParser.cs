using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiling
{
    class MultipliNodeParser : IInfixParser
    {
        public MultipliNodeParser(KeyParser parser)
        {
            Register(parser);
        }
        public int GetPrecedence()
        {
            return Precedence.PRODUCT_DIVISION;
        }

        public Expression Parse(KeyParser parser, Expression left, Token token,List<CompilingError> errors)
        {
            Expression right = parser.ParseExpression(errors,GetPrecedence());

            if (right == null)
            {
                errors.Add(new CompilingError(token.Location, ErrorCode.Invalid, "Rigth expression of the multiplication"));
                return null;
            }

            return new MultiplicationNode(token.Location,left, right);
        }

        public void Register(KeyParser Parser)
        {
            Parser.RegisterInfixParserByValue(TokenValues.Mul, this);
        }
    }
}
