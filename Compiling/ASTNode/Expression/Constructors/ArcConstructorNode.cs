using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiling
{
    public class ArcConstructorNode : Expression
    {
        Expression p1;
        Expression p2;
        Expression p3;
        Expression measure;

        public ArcConstructorNode(CodeLocation location, Expression p1, Expression p2, Expression p3, Expression measure)
        {
            this.p1 = p1;
            this.p2 = p2;
            this.p3 = p3;
            this.measure = measure;
            this.Location = location;

        }

        public override bool CheckSemantics(ScopeCheckSemantic scope, List<CompilingError> errors)

        {

            bool a1 = p1.CheckSemantics(scope, errors);
            bool a2 = p2.CheckSemantics(scope, errors);
            bool a3 = p3.CheckSemantics(scope, errors);
            bool m1 = measure.CheckSemantics(scope, errors);

            if (!a1 || !a2 || !a3 || !m1)
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
            GeoPoint aux3 = p3.EvaluateOn(scope) as GeoPoint;
            IGeoEntities aux4 = measure.EvaluateOn(scope);

            if (aux1 == null || aux2 == null || aux3 == null)
            {
                scope.Errors.Add(new CompilingError(p1.Location, ErrorCode.Invalid, "Expression"));
                return null;
            }

            if (aux4 as Measure != null)
                return new Arc(aux1, aux2, aux3, aux4 as Measure);

            if (aux4 as Number != null)
            {
                Number use = aux4 as Number;
                return new Arc(aux1, aux2, aux3, new Measure(use.Value));
            }


            scope.Errors.Add(new CompilingError(measure.Location, ErrorCode.Expected, "Measure"));
            return null;


        }
    }
}
