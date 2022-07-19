﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiling
{
    public class GreaterOrEqualsNodeParser : IInfixParser
    {
        public GreaterOrEqualsNodeParser(KeyParser parser)
        {
            Register(parser);
        }
        public int GetPrecedence()
        {
            return Precedence.CONDITIONAL;
        }

        public Expression Parse(KeyParser parser, Expression left, Token token, List<CompilingError> errors)
        {
            Expression right = parser.ParseExpression(errors,GetPrecedence());

            if (right == null)
            {
                errors.Add(new CompilingError(token.Location, ErrorCode.Invalid, "Expression"));
                return null;
            }

            return new GreaterOrEqualNode(token.Location,left, right);
        }

        public void Register(KeyParser parser)
        {
            parser.RegisterInfixParserByValue(TokenValues.GreaterOrEquals, this);
        }
    }
}
