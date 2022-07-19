using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiling
{
    public class OrNode : Boolean
    {
        
        public OrNode(CodeLocation location,Expression left,Expression right)
        {
            Left = left;
            Right = right;
            this.Location = location;
        }
        public override IGeoEntities EvaluateOn(ScopeExecution scope)
        {
            IGeoEntities g1 = Left.EvaluateOn(scope);
            IGeoEntities g2 = Right.EvaluateOn(scope);
            IGeoEntities aux1 = new Number();
            IGeoEntities aux2 = new Undefined();
            IGeoEntities aux3 = new Sequence();

            if (g1 == null || g2 == null)
            {
                scope.Errors.Add(new CompilingError(Left.Location, ErrorCode.Invalid, "Expression"));
                return null;
            }

            if ((g1 != aux1 && g1 != aux2 && g1 != aux3) || (g2 != aux1 && g2 != aux2 && g2 != aux3))
                return new Number(1);

            return new Number();
        }
    }
}
