using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiling
{
    public class PlusUnaryOperatorNode : UnaryOperatorNode
    {
        
        public PlusUnaryOperatorNode(CodeLocation location,Expression Body)
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
                scope.Errors.Add(new CompilingError(ToModify.Location, ErrorCode.Invalid, "Invalid parameter"));
                return null;
            }
            Number aux1 = aux as Number;
            Measure aux2 = aux as Measure;

            if (aux1 != null) return aux1;
            if (aux2 != null) return aux2;

            scope.Errors.Add(new CompilingError(ToModify.Location, ErrorCode.Invalid, "This operation is not valid for the given expression"));
            return null;
        }
    }
}
