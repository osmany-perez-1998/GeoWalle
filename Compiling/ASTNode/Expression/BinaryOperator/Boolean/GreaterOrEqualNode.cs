using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiling
{
    public class GreaterOrEqualNode : Boolean
    {
        
        public GreaterOrEqualNode(CodeLocation location,Expression left, Expression right)
        {
            Left = left;
            Right = right;
            this.Location = location;
        }
        public override IGeoEntities EvaluateOn(ScopeExecution scope)
        {
            IGeoEntities aux1 = Left.EvaluateOn(scope);
            IGeoEntities aux2 = Right.EvaluateOn(scope);
            Number n1 = aux1 as Number;
            Number n2 = aux2 as Number;
            Measure m1 = aux1 as Measure;
            Measure m2 = aux2 as Measure;
            if (aux1 == null || aux2 == null)
            {
                scope.Errors.Add(new CompilingError(Left.Location, ErrorCode.Invalid, "Expression"));
                return null;
            }

            if (n1 != null && n2 != null) return n1.Value >= n2.Value ? new Number(1) : new Number(0);
            if (m1 != null && m2 != null) return m1.Value >= m2.Value ? new Number(1) : new Number(0);

            scope.Errors.Add(new CompilingError(Left.Location, ErrorCode.Invalid, "Cannot be compared"));
            return null;
        }
    }
}
