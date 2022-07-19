using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiling
{
    public class InfiniteSequenceNode : Expression
    {
        Expression nm1;
        
        public InfiniteSequenceNode(CodeLocation location, Expression nm1)
        {
            this.nm1 = nm1;
            this.Location = location;
        }
        public override bool CheckSemantics(ScopeCheckSemantic scope, List<CompilingError> errors)
        {
            bool left = nm1.CheckSemantics(scope, errors);

            if (!left)
            {
                errors.Add(new CompilingError(nm1.Location, ErrorCode.Invalid, "Semantic"));
                return false;
            }
            return true;
        }

        public override IGeoEntities EvaluateOn(ScopeExecution scope)
        {
            
            IGeoEntities aux = nm1.EvaluateOn(scope);
            Number aux_1 = aux as Number;
            if (aux_1 == null)
            {
                scope.Errors.Add(new CompilingError(nm1.Location, ErrorCode.Invalid, "Invalid parameter"));
                return null;
            }
            int a;
            if (int.TryParse(aux_1.Value.ToString(), out a)) return new Sequence(new Number(a));


            scope.Errors.Add(new CompilingError(nm1.Location, ErrorCode.Expected, "A number of type int"));
            return null;


        }
    }
}
