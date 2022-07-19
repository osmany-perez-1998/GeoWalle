using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiling
{
    public class DrawNode:Statement
    {

        Expression body;
        string label;
        CodeLocation location;
        public DrawNode(CodeLocation location, Expression body, string label="")
        {
            this.body = body;
            this.label = label;
            this.location = location;
        }

        public override bool CheckSemantics(ScopeCheckSemantic scope, List<CompilingError> errors)
        {
           if(!body.CheckSemantics(scope, errors))
            {
                errors.Add(new CompilingError(location, ErrorCode.Invalid, "Expression"));
                return false;
            }
            return true;
        }

        public override void Execute(ScopeExecution scope,IDrawer drawer)
        {
            IGeoEntities toPaint = body.EvaluateOn(scope);

            if(toPaint==null || toPaint as IPaintable == null)
            {
                scope.Errors.Add(new CompilingError(location, ErrorCode.Invalid, "Expression could not be drawn"));
            }
            string color;
            if (scope.Colors.Count == 0) color = "Black";
            else color = scope.Colors.Peek();

            if (toPaint is IPaintable) drawer.Paint(toPaint,color, label);
        }
    }
}
