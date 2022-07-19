using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiling
{
    public class SubtractionNode : BinaryOperatorNode
    {
       
        public SubtractionNode(CodeLocation location,Expression a1,Expression a2)
        {
            Left = a1;
            Right = a2;
            this.Location = location;
        }
        public override bool CheckSemantics(ScopeCheckSemantic scope, List<CompilingError> errors)
        {
           
            return Left.CheckSemantics(scope,errors) && Right.CheckSemantics(scope,errors);
        }

        public override IGeoEntities EvaluateOn(ScopeExecution scope)
        {
            IGeoEntities left = Left.EvaluateOn(scope);
            IGeoEntities right = Right.EvaluateOn(scope);

            if (left == null || right == null)
            {
                scope.Errors.Add(new CompilingError(Left.Location, ErrorCode.Invalid, "Expression"));
                return null;
            }

            if ((Number)left != null && (Number)right != null)
                return Operations.Substract((Number)left, (Number)right);

            if ((Measure)left != null && (Measure)right != null)
                return Operations.Substract((Measure)left, (Measure)right);

            scope.Errors.Add(new CompilingError(Left.Location, ErrorCode.Invalid, "These expressions cannot be subtracted"));
            return null;

        }
    }
}
