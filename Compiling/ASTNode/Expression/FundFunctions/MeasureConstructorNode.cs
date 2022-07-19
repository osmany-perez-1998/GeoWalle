using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiling
{
    public class MeasureConstructorNode : Expression
    {
        Expression p1, p2;
        
        public MeasureConstructorNode(CodeLocation location,Expression p1, Expression p2)
        {
            this.p1 = p1;
            this.p2 = p2;
            this.Location = location;
            
        }
        public override bool CheckSemantics(ScopeCheckSemantic scope, List<CompilingError> errors)

        {
            bool left = p1.CheckSemantics(scope, errors);
            bool right = p2.CheckSemantics(scope, errors);

            if (!left || !right)
            {
                errors.Add(new CompilingError(p1.Location, ErrorCode.Invalid, "Semantic"));
                return false;
            }
            return true;
        }

        public override IGeoEntities EvaluateOn(ScopeExecution scope)
        {
            GeoPoint aux1 = p1.EvaluateOn(scope) as GeoPoint;
            GeoPoint aux2 = p2.EvaluateOn(scope) as GeoPoint;

            if (aux1 == null || aux2 == null)
            {
                scope.Errors.Add(new CompilingError(p1.Location, ErrorCode.Invalid, "Expression"));
                return null;
            }

            return new Measure(aux1, aux2);
        }
    }
}
