using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiling
{
    public class IntersectConstructorNode : Expression
    {
        Expression geo1, geo2;

        public IntersectConstructorNode(CodeLocation location, Expression p1, Expression p2)
        {
            geo1 = p1;
            geo2 = p2;
            this.Location = location;
        }

        public override bool CheckSemantics(ScopeCheckSemantic scope, List<CompilingError> errors)
        {
            bool left = geo1.CheckSemantics(scope, errors);
            bool right = geo2.CheckSemantics(scope, errors);

            if (!left || !right)
            {
                errors.Add(new CompilingError(geo1.Location, ErrorCode.Invalid, "Semantic"));
                return false;
            }
            return true;
        }

        public override IGeoEntities EvaluateOn(ScopeExecution scope)
        {
            IGeoEntities aux1 = geo1.EvaluateOn(scope);
            IGeoEntities aux2 = geo2.EvaluateOn(scope);


            if (aux1 == null || aux2 == null)
            {
                scope.Errors.Add(new CompilingError(geo1.Location, ErrorCode.Invalid, "Expression"));
                return null;
            }

            if (aux1 is IIntersectable && aux2 is IIntersectable)
            {
                return Intersections.Intersect(aux1, aux2);
            }

            scope.Errors.Add(new CompilingError(geo1.Location, ErrorCode.Invalid, "Parameters"));
            return null;
        }
    }
}
