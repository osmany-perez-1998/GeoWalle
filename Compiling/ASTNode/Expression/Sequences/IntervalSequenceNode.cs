using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiling
{
    public class IntervalSequenceNode : Expression
    {
        Expression nm1, nm2;
        
        public IntervalSequenceNode(CodeLocation location, Expression nm1,Expression nm2)
        {
            this.nm1 = nm1;
            this.nm2 = nm2;
            this.Location = location;
        }
        public override bool CheckSemantics(ScopeCheckSemantic scope, List<CompilingError> errors)
        {


            bool left = nm1.CheckSemantics(scope, errors);
            bool right = nm2.CheckSemantics(scope, errors);

            if (!left || !right)
            {
                errors.Add(new CompilingError(nm1.Location, ErrorCode.Invalid, "Semantic"));
                return false;
            }
            return true;

        }

        public override IGeoEntities EvaluateOn(ScopeExecution scope)
        {
            IGeoEntities aux = nm1.EvaluateOn(scope);
            ConstantNode aux_1 = aux as ConstantNode;
            IGeoEntities aux2 = nm2.EvaluateOn(scope);
            ConstantNode aux_2 = aux2 as ConstantNode;
           
                if (aux_1 == null || aux_2 == null)
                {
                    scope.Errors.Add(new CompilingError(nm1.Location, ErrorCode.Expected, "Number"));
                    return null;
                }
            int a;
            int b;
            if (int.TryParse(aux_1.Value.ToString(), out a) && int.TryParse(aux_2.Value.ToString(), out b)) return new Sequence(new Number(a), new Number(b));


            scope.Errors.Add(new CompilingError(nm1.Location, ErrorCode.Expected, "A number of type int"));
            return null;
        }
    }
}
