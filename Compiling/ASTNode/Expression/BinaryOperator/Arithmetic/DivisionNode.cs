using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiling
{
    class DivisionNode : BinaryOperatorNode
    {
        
        public DivisionNode(CodeLocation location,Expression Right,Expression Left)
        {
            this.Right = Right;
            this.Left = Left;
            this.Location = location;
        }
        public override bool CheckSemantics(ScopeCheckSemantic scope,List<CompilingError>errors)

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

            if (left == null || right == null)
            {
                scope.Errors.Add(new CompilingError(Left.Location, ErrorCode.Invalid, "Expression"));
                return null;
            }

            if (left as Number!= null && right as Number != null)
                return Operations.Division((Number)left, (Number)right);

            if (left as Measure != null && right as Measure != null)
                return Operations.Division((Measure)left, (Measure)right);

            scope.Errors.Add(new CompilingError(Left.Location, ErrorCode.Invalid, "These expressions cannot be divided"));
            return null;
        }
    }
}
