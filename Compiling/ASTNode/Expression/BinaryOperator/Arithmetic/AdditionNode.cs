using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiling
{
    public class AdditionNode : BinaryOperatorNode
    {
       
        public AdditionNode(CodeLocation location,Expression left,Expression right)
        {
            Left = left;
            Right = right;
            this.Location = location;
        }
        public override bool CheckSemantics(ScopeCheckSemantic scope, List<CompilingError> errors)
        {
            bool left = Left.CheckSemantics(scope, errors);
            bool right = Right.CheckSemantics(scope, errors);

            if (!left || !right)
            {
                errors.Add(new CompilingError(Left.Location, ErrorCode.Invalid, "Semantic"));
                return false;
            }
            return true;


        }

        public override IGeoEntities EvaluateOn(ScopeExecution scope)
        {
            IGeoEntities left = Left.EvaluateOn(scope);
            IGeoEntities right = Right.EvaluateOn(scope);

            if(left==null || right == null)
            {
                scope.Errors.Add(new CompilingError(Left.Location, ErrorCode.Invalid, "Expression"));
                return null;
            }

            if (left as Number != null && right as Number != null)
                return Operations.Add((Number)left,(Number) right);

            if (left as Measure != null && right as Measure != null)
                return Operations.Add((Measure)left, (Measure)right);

            scope.Errors.Add(new CompilingError(Left.Location,ErrorCode.Invalid,"These expression cannot be added"));
            return null;
        }
    }
}
