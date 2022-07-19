﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiling
{
    class MinusNodeParser : IInfixParser
    {

        public MinusNodeParser(KeyParser parser)
        {
            Register(parser);
        }
        public int GetPrecedence()
        {
            return Precedence.SUM_SUBSTRACT;
        }
        public Expression Parse(KeyParser parser, Expression left, Token token, List<CompilingError> errors)
        {
            Expression right = parser.ParseExpression(errors, GetPrecedence());
            if (right == null)
            {
                errors.Add(new CompilingError(token.Location, ErrorCode.Invalid, "Right expression of the substraction"));
                return null;
            }
            return new SubtractionNode(token.Location, left, right);
        }

        public void Register(KeyParser parser)
        {
            parser.RegisterInfixParserByValue(TokenValues.Sub, this);
        }
    }
}
