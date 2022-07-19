using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiling
{
    public class AndNode : Boolean
    {
       
        public AndNode(CodeLocation location, Expression left,Expression right)
        {
            Left = left;
            Right = right;
            this.Location = location;
        }
        public override IGeoEntities EvaluateOn(ScopeExecution scope)
        {
            IGeoEntities g1 = Left.EvaluateOn(scope);
            IGeoEntities g2 = Right.EvaluateOn(scope);
            Number aux1 = g1 as Number;
            Number aux1_1 = g2 as Number;
            Undefined aux2 = g1 as Undefined;
            Undefined aux2_1 = g2 as Undefined;
            Sequence aux3 =g1 as Sequence;
            Sequence aux3_1 =g2 as Sequence;

            if (g1 == null || g2 == null)
            {
                scope.Errors.Add(new CompilingError(Left.Location, ErrorCode.Invalid, "Expression"));
                return null;
            }

            if (aux1!= null && aux1.Value==0  || aux1_1 != null && aux1_1.Value == 0 || aux2!=null || aux2_1 !=null || aux3 !=null && aux3.InsideType=="empty" || aux3_1 != null && aux3_1.InsideType == "empty")
                return new Number();

            return new Number(1);
        }
    }
}
