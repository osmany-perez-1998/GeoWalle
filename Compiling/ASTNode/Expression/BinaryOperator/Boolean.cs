using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiling
{
    abstract public class Boolean:BinaryOperatorNode
    {
        public override bool CheckSemantics(ScopeCheckSemantic scope, List<CompilingError> errors)
        {
            bool left = Left.CheckSemantics(scope, errors);
              bool right=  Right.CheckSemantics(scope,errors);

            if (!left || !right)
            {
                errors.Add(new CompilingError(Left.Location, ErrorCode.Invalid, "Semantic"));
                return false;
            }
            return true;

        }
    }
}
