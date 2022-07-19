using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiling
{
    public class CircleConstructorNode : Expression
    {
        Expression center, radius;

        public CircleConstructorNode(CodeLocation location, Expression center, Expression radius)
        {
            this.center = center;
            this.radius = radius;
            this.Location = location;
        }
        public override bool CheckSemantics(ScopeCheckSemantic scope, List<CompilingError> errors)
        {
            bool left = center.CheckSemantics(scope, errors);
            bool right = radius.CheckSemantics(scope, errors);

            if (!left || !right)
            {
                errors.Add(new CompilingError(center.Location, ErrorCode.Invalid, "Semantic"));
                return false;
            }
            return true;


        }

        public override IGeoEntities EvaluateOn(ScopeExecution scope)
        {
            GeoPoint aux1 = center.EvaluateOn(scope) as GeoPoint;
            IGeoEntities aux2 = radius.EvaluateOn(scope);
            if (aux1 == null || aux2 == null)
            {

                scope.Errors.Add(new CompilingError(center.Location, ErrorCode.Invalid, "Expression"));
                return null;
            }



            if (aux2 as Measure != null) return new Circle(aux1, aux2 as Measure);
            if (aux2 as Number != null) return new Circle(aux1, new Measure((aux2 as Number).Value));

            scope.Errors.Add(new CompilingError(center.Location, ErrorCode.Invalid, "Parameters"));
            return null;

        }
    }
}
