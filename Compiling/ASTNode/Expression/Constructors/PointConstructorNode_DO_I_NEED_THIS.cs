using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiling
{
    public class PointConstructorNode : Expression
    {
        Expression nm1, nm2;
       
        public PointConstructorNode(CodeLocation location,Expression nm1, Expression nm2)
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
            Number x = nm1.EvaluateOn(scope) as Number;
            Number y = nm2.EvaluateOn(scope) as Number;

            if (x == null || x == null)
            {
                scope.Errors.Add(new CompilingError(nm1.Location, ErrorCode.Invalid, "Expression"));
                return null;
            }

            return new GeoPoint(x.Value, y.Value);
        }
    }
}
