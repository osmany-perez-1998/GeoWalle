using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiling
{
    public class EqualNode : Boolean
    {
        
        public EqualNode(CodeLocation location,Expression left, Expression right)
        {
            Left = left;
            Right = right;
            this.Location = location;
        }

        public override IGeoEntities EvaluateOn(ScopeExecution scope)
        {
            IGeoEntities left = Left.EvaluateOn(scope);
            IGeoEntities right = Right.EvaluateOn(scope);

            if (left == null || right == null)
            {
                scope.Errors.Add(new CompilingError(Left.Location, ErrorCode.Invalid, "Expression"));
                return null;
            }

            bool equal = Left.EvaluateOn(scope) == Right.EvaluateOn(scope);

            return equal ? new Number(1) : new Number(0);
        }
    }
}
