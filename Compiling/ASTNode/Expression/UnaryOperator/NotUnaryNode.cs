using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiling
{
    public class NotUnaryNode : UnaryOperatorNode
    {

        
        public NotUnaryNode(CodeLocation location, Expression Body)
        {
            ToModify = Body;
            this.Location = location;
        }
        public override bool CheckSemantics(ScopeCheckSemantic scope, List<CompilingError> errors)
        {
            bool left = ToModify.CheckSemantics(scope, errors);

            if (!left)
            {
                errors.Add(new CompilingError(ToModify.Location, ErrorCode.Invalid, "Semantic"));
                return false;
            }
            return true;
        }

        public override IGeoEntities EvaluateOn(ScopeExecution scope)
        {
            IGeoEntities aux = ToModify.EvaluateOn(scope);

            if (aux == null)
            {
                scope.Errors.Add(new CompilingError(ToModify.Location, ErrorCode.Invalid, "Expression"));
                return null;
            }

            Number aux1 = aux as Number;
            Undefined aux2 = aux as Undefined;
            Sequence aux3 = aux as Sequence;

            if (aux1 != null && aux1.Value == 0 || aux2 != null || aux3 != null && aux3.InsideType == "empty")
                return new Number(1);
            return new Number(0);
        }
    }
}
